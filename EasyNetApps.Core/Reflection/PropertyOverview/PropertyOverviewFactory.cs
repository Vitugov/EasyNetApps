using EasyNetApps.Core.Reflection.Properties;
using EasyNetApps.Core.Reflection.ReflectionOperations;
using EasyNetApps.Core.Reflection.UserClasses;
using System.Reflection;
using static EasyNetApps.Core.Reflection.Delegates.Delegates;

namespace EasyNetApps.Core.Reflection.PropertyOverview
{
    public class PropertyOverviewFactory(
        PropertyOverviewCreator propertyOverviewCreator,
        IUserClasses userClasses,
        IPropertyReflectionOperations operations)
        : IPropertyOverviewFactory
    {
        private readonly PropertyOverviewCreator _propertyOverviewCreator = propertyOverviewCreator;
        private readonly IUserClasses _userClasses = userClasses;
        private readonly IPropertyReflectionOperations _operations = operations;

        public IPropertyOverview Create(PropertyInfo property)
        {
            return _propertyOverviewCreator(property, _userClasses, _operations);
        }
    }
}
