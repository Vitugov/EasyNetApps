using EasyNetApps.Core.Reflection.UserClassesOverviews;
using EasyNetApps.Core.Reflection.UserEntityInterface;
using EasyNetApps.DbAccess.QueryModifications.QueryParameters;
using Microsoft.EntityFrameworkCore;

namespace EasyNetApps.DbAccess.QueryModifications
{
    public class UserTypeNonCollectionProperties(IUserClassesOverviews userClassesOverviews) : IQueryModifiaction<EmptyParameters>
    {
        private readonly IUserClassesOverviews _userClassesOverviews = userClassesOverviews;

        public IQueryable<T> Apply<T>(IQueryable<T> query, EmptyParameters? parameters = null)
            where T : class, IProjectModel
        {
            var classOverview = _userClassesOverviews[typeof(T)];
            foreach (var propertyOverview in classOverview.UserClassNotCollectionProperties)
            {
                query = query.Include(propertyOverview.Name);
            }
            return query;
        }
    }
}
