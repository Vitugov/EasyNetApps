using EasyNetApps.Core.Reflection.PropertyOverview;
using EasyNetApps.Core.Reflection.ReflectionOperations;
using System.Reflection;
using static EasyNetApps.Core.Reflection.Delegates.Delegates;

namespace EasyNetApps.Core.Reflection.ClassOverview
{
    public class ClassOverviewFactory(
        IPropertyOverviewFactory propertyOverviewFactory,
        ClassOverviewCreator classOverviewCreator,
        IClassReflectionOperations operations)
        : IClassOverviewFactory
    {
        private readonly IPropertyOverviewFactory _propertyOverviewFactory = propertyOverviewFactory;
        private readonly ClassOverviewCreator _classOverviewCreator = classOverviewCreator;
        private readonly IClassReflectionOperations _operations = operations;

        public IClassOverview Create(Type userClass)
        {
            var properties = userClass.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var propertyOverviews = properties.Select(property => _propertyOverviewFactory.Create(property)).ToList();
            return _classOverviewCreator(userClass, propertyOverviews, _operations);
        }
    }
}
