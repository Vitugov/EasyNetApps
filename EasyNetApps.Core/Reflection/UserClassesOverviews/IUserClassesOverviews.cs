using EasyNetApps.Core.Reflection.ClassOverview;

namespace EasyNetApps.Core.Reflection.UserClassesOverviews
{
    public interface IUserClassesOverviews
    {
        IClassOverview this[Type type] { get; }
        IClassOverview this[string typeName] { get; }
        IEnumerable<IClassOverview> AllOverviews { get; }
    }
}