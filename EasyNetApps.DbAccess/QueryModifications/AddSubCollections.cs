using EasyNetApps.Core.Reflection.UserClassesOverviews;
using EasyNetApps.Core.Reflection.UserEntityInterface;
using EasyNetApps.DbAccess.QueryModifications.QueryParameters;
using Microsoft.EntityFrameworkCore;

namespace EasyNetApps.DbAccess.QueryModifications
{
    public class AddSubCollections(IUserClassesOverviews userClassesOverviews) : IQueryModifiaction<EmptyParameters>
    {
        private readonly IUserClassesOverviews _userClassesOverviews = userClassesOverviews;

        public IQueryable<T> Apply<T>(IQueryable<T> query, EmptyParameters? parameters = null)
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
