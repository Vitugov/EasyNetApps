using EasyNetApps.Core.Reflection.Properties;
using EasyNetApps.Core.Reflection.UserClassesOverviews;
using EasyNetApps.Core.Reflection.UserEntityInterface;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace EasyNetApps.DbAccess.Old
{
    public class DbHandler(IUserClassesOverviews classesOverviews)
    {
        private readonly IUserClassesOverviews _classesOverviews = classesOverviews;

        public List<ProjectModel> GetSet(Type type)
        {
            using (var context = DbContextCreator.Create())
            {
                return context.Set(type).ToList();
            }
        }

        public T GetDeepSetForObj<T>(T obj)
            where T : class, IProjectModel
        {
            using (var context = DbContextCreator.Create())
            {
                var query = context.DeepSet<T>().Where(e => e.Id == obj.Id);
                return query.First();
            }
        }

        public List<T> GetShallowSetList<T>()
            where T : class, IProjectModel
        {
            using (var context = DbContextCreator.Create())
            {
                var query = context.ShallowSet<T>();
                return query.ToList();
            }
        }

        public void DeleteItem<T>(T item)
            where T : class, IProjectModel
        {
            var classOverview = _classesOverviews[typeof(T)];
            var collectionProperties = classOverview.UserClassCollectionProperties;
            //todo: don't forget about subclasses
            //    .Where(propertyOverview => propertyOverview.IsGenericClassSubClass);
            using (var context = DbContextCreator.Create())
            {
                var query = context.DeepSet<T>().Where(e => e.Id == item.Id);
                item = query.First();
                foreach (var collectionProperty in collectionProperties)
                {
                    var itemValueCollection = GetPropertyValueForItem(collectionProperty, item);

                    foreach (var row in itemValueCollection)
                    {
                        context.Entry(row).State = EntityState.Deleted;
                    }
                }
                context.Set<T>().Remove(item);
                context.SaveChanges();
            }
        }

        public void SaveItem<T>(T edit, T original, bool isNew)
            where T : class, IProjectModel
        {
            InvokeSaver(typeof(SubCollectionSavePreparer<>), edit, original);

            original.UpdateFrom(edit);

            using (var context = DbContextCreator.Create())
            {
                var dbSet = context.Set<T>();
                dbSet.Update(original);
                if (isNew)
                {
                    context.Entry(original).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public void SaveSubCollection<T>(List<T> toAdd, List<T> toUpdate, List<T> toDelete)
            where T : class, IProjectModel
        {
            using (var DbContext = DbContextCreator.Create())
            {
                var dbSet = DbContext.Set<T>();
                dbSet.RemoveRange(toDelete);
                toAdd.ForEach(item => DbContext.Entry(item).State = EntityState.Added);
                dbSet.UpdateRange(toUpdate);
                DbContext.SaveChanges();
            }
        }

        public List<ProjectModel> GetLinksOnItem<T>(T item)
            where T : class, IProjectModel
        {
            var linksList = new List<ProjectModel>();
            foreach (var userClassOverview in _classesOverviews.AllOverviews)
            {
                var propertyOverviewOfType = userClassOverview.GetPropertiesOfType(typeof(T));
                foreach (var propertyOverview in propertyOverviewOfType)
                {
                    List<ProjectModel> resultList;
                    using (var DbContext = DbContextCreator.Create())
                    {
                        resultList = DbContext
                            .ShallowSet(userClassOverview.Type)
                            .ToList()
                            .Where(obj => ((T)propertyOverview.Property.GetValue(obj)).Id == item.Id)
                            .ToList();
                    }
                    if (resultList.Any())
                    {
                        linksList.AddRange(resultList);
                    }
                }
            }
            return linksList;
        }

        public void InvokeSaver<T>(Type type, T edit, T original)
            where T : class, IProjectModel
        {
            var classOverview = _classesOverviews[typeof(T)];
            var collections = classOverview.UserClassCollectionProperties;
            foreach (var collection in collections)
            {
                //todo: don't forget about SubClass
                //if (collection.IsGenericClassSubClass)
                //{
                var editCollection = GetPropertyValueForItem(collection, edit);
                var originalCollection = GetPropertyValueForItem(collection, original);
                Type genericListType = type.MakeGenericType(collection.GenericOfIEnumerable!);
                Activator.CreateInstance(genericListType, editCollection, originalCollection);
                //}
            }
        }

        private IList GetPropertyValueForItem<T>(IPropertyOverview propertyOverview, T item)
            where T : class, IProjectModel
        {
            return (IList)propertyOverview.Property.GetValue(item)!;
        }
    }
}
