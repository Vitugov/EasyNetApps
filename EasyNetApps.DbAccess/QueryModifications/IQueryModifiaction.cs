using EasyNetApps.Core.Reflection.UserEntityInterface;

namespace EasyNetApps.DbAccess.QueryModifications
{
    public interface IQueryModifiaction<TParameters>
    {
        IQueryable<T> Apply<T>(IQueryable<T> query, TParameters parameters)
            where T : class, IProjectModel;
    }
}
