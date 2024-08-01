using EasyNetApps.Core.Reflection.ClassOverview;

namespace EasyNetApps.Core.Reflection.UserClassesOverviews
{
    public class UserClassesOverviews : IUserClassesOverviews
    {
        private readonly Dictionary<string, IClassOverview> _classOverviewDic = [];

        public UserClassesOverviews(IEnumerable<IClassOverview> classOverviewCollection)
        {
            classOverviewCollection
                .ToList()
                .ForEach(classOverview => _classOverviewDic[classOverview.Name] = classOverview);
        }

        public IClassOverview this[Type type] => _classOverviewDic[type.Name];
        public IClassOverview this[string typeName] => _classOverviewDic[typeName];
        public IEnumerable<IClassOverview> AllOverviews => _classOverviewDic.Values;
    }
}
