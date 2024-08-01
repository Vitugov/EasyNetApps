using EasyNetApps.Core.Reflection.UserClassesOverviews;
using EasyNetApps.Core.Reflection.UserEntityInterface;
using Microsoft.EntityFrameworkCore;

namespace EasyNetApps.DbAccess.Old
{
    public class GetQueries(IUserClassesOverviews userClassesOverviews)
    {
        private readonly IUserClassesOverviews _userClassesOverviews = userClassesOverviews;

        public IQueryable<IProjectModel> Set(DbContext dbContext, Type type)
        {
            var method = dbContext.GetType().GetMethods()
                .Single(p => p.Name == nameof(DbContext.Set) && p.ContainsGenericParameters && p.GetParameters().Length == 0);
            method = method.MakeGenericMethod(type);

            var result = (IQueryable<IProjectModel>)method.Invoke(dbContext, null)!;
            return result;
        }

        public IQueryable<T> ShallowSet<T>(DbContext dbContext)
            where T : class, IProjectModel
        {
            var query = dbContext.Set<T>();
            return AddUserTypeNonCollectionProperties(query, typeof(T));
        }

        public IQueryable<IProjectModel> ShallowSet(DbContext dbContext, Type type)
        {
            var query = Set(dbContext, type);
            return AddUserTypeNonCollectionProperties(query, type);
        }

        public IQueryable<T> DeepSet<T>(DbContext dbContext)
            where T : ProjectModel
        {
            IQueryable<T> query = dbContext.Set<T>();
            AddUserTypeNonCollectionProperties(query, typeof(T));
            return AddSubCollections(ref query);
        }


        private IQueryable<T> AddUserTypeNonCollectionProperties<T>(IQueryable<T> query, Type type)
            where T : class, IProjectModel
        {
            var classOverview = _userClassesOverviews[type];
            foreach (var propertyOverview in classOverview.UserClassNotCollectionProperties)
            {
                query = query.Include(propertyOverview.Name);
            }
            return query;
        }

        private IQueryable<T> AddSubCollections<T>(ref IQueryable<T> query)
            where T : class, IProjectModel
        {
            var classOverview = _userClassesOverviews[typeof(T)];
            //todo: don't forget about subclass!!!
            foreach (var propertyOverview in classOverview.UserClassCollectionProperties)
            {
                var genericType = propertyOverview.GenericOfIEnumerable!;
                foreach (var subPropertyOverview in _userClassesOverviews[genericType].UserClassNotCollectionProperties)
                {
                    query = query.Include($"{propertyOverview.Name}.{subPropertyOverview.Name}");
                }
            }
            return query;
        }
    }
}
