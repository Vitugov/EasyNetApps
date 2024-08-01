using EasyNetApps.Core.Attributes;
using EasyNetApps.Core.Reflection.ClassOverview;
using EasyNetApps.Core.Reflection.UserClasses;
using System.Reflection;

namespace EasyNetApps.Core.Reflection.UserClassesOverviews
{
    public class UserClassesOverviews : IUserClassesOverviews
    {
        private Dictionary<string, IClassOverview> ClassOverviewDic { get; set; } = [];
        private IUserClasses UserClasses { get; set; }
        private List<KeyValuePair<string, Type>> MainWindowClasses { get; set; } = [];

        public UserClassesOverviews(IUserClasses userClasses, IEnumerable<IClassOverview> classOverviewCollection)
        {
            UserClasses = userClasses;
            MainWindowClasses = GetTypesForMainWindow();
            FillDictionary();
        }

        private List<KeyValuePair<string, Type>> GetTypesForMainWindow()
        {
            return UserClasses.Items
                .Where(type => type.GetCustomAttribute<SubClassAttribute>() == null)
                .Select(type => new KeyValuePair<string, Type>(type.GetCustomAttribute<DisplayNamesAttribute>().Plural, type))
                .OrderBy(pair => pair.Key)
                .ToList();
        }

        private void FillDictionary()
        {
            foreach (var derivedClass in UserClasses.Items)
            {
                if (!ClassOverviewDic.ContainsKey(derivedClass.Name))
                {
                    var classOverview = new ClassOverview(derivedClass);
                    ClassOverviewDic[classOverview.Name] = classOverview;
                }
            }
        }

        private ClassOverview GetClassOverview(string name)
        {
            var IsSuccess = ClassOverviewDic.TryGetValue(name, out ClassOverview? classOverView);
            if (!IsSuccess || classOverView == null)
            {
                throw new Exception($"Dictionary doesn't contain class {name}.");
            }
            return classOverView;
        }

        //public bool Contains(this Type type)
        //{
        //    return UserClasses.Items.Contains(type);
        //}

        public List<KeyValuePair<string, Type>> GetMainWindowClasses()
        {
            return MainWindowClasses;
        }

        public List<ClassOverview> GetAllUserClassesOverview()
        {
            return ClassOverviewDic.Values.ToList();
        }
    }
}
