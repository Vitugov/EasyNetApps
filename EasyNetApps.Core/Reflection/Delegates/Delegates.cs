using EasyNetApps.Core.Reflection.ClassOverview;
using EasyNetApps.Core.Reflection.Properties;
using EasyNetApps.Core.Reflection.ReflectionOperations;
using EasyNetApps.Core.Reflection.UserClasses;
using EasyNetApps.Core.Reflection.UserClassesOverviews;
using System.Reflection;

namespace EasyNetApps.Core.Reflection.Delegates
{
    public class Delegates
    {
        public delegate IUserClassesOverviews UserClassesOverviewsCreator(IEnumerable<IClassOverview> classOverviews);
        public delegate IClassOverview ClassOverviewCreator
            (Type userClass, IEnumerable<IPropertyOverview> propertyOverviews, IClassReflectionOperations operations);
        public delegate IPropertyOverview PropertyOverviewCreator
            (PropertyInfo property, IUserClasses userClasses, IPropertyReflectionOperations operations);
    }
}
