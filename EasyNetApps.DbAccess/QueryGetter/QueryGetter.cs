using EasyNetApps.Core.Reflection.UserEntityInterface;
using Microsoft.EntityFrameworkCore;

namespace EasyNetApps.DbAccess.QueryGetter
{
    public class QueryGetter : IQueryGetter
    {
        public IQueryable<T> Get<T>(DbContext dbContext)
            where T : class, IProjectModel
        {
            return dbContext.Set<T>();
        }

        public IQueryable<IProjectModel> Get(DbContext dbContext, Type type)
        {
            var method = dbContext.GetType().GetMethods()
                .Single(p => p.Name == nameof(DbContext.Set) && p.ContainsGenericParameters && p.GetParameters().Length == 0);
            method = method.MakeGenericMethod(type);

            var result = (IQueryable<IProjectModel>)method.Invoke(dbContext, null)!;
            return result;
        }
    }
}
