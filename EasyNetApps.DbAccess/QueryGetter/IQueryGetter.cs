using EasyNetApps.Core.Reflection.UserEntityInterface;
using Microsoft.EntityFrameworkCore;

namespace EasyNetApps.DbAccess.QueryGetter
{
    public interface IQueryGetter
    {
        IQueryable<IProjectModel> Get(DbContext dbContext, Type type);
        IQueryable<T> Get<T>(DbContext dbContext) where T : class, IProjectModel;
    }
}