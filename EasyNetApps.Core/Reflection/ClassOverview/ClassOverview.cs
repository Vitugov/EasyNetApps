using EasyNetApps.Core.Reflection.Properties;
using EasyNetApps.Core.Reflection.ReflectionOperations;

namespace EasyNetApps.Core.Reflection.ClassOverview
{
    public class ClassOverview : IClassOverview
    {
        private readonly IClassReflectionOperations _operations;

        public Type Type { get; }
        public string Name { get; }
        public string DisplayNameSingular { get; }
        public string DisplayNamePlural { get; }
        public bool IsSubClass { get; }

        public bool ShowInMainWindow { get; }
        public List<IPropertyOverview> Properties { get; }
        public List<IPropertyOverview> UserClassProperties { get; }
        public List<IPropertyOverview> UserClassNotCollectionProperties { get; }
        public List<IPropertyOverview> UserClassCollectionProperties { get; }
        public List<IPropertyOverview> GetPropertiesOfType(Type type) =>
            Properties
                .Where(propertyOverview => propertyOverview.Property.PropertyType == type)
                .ToList();

        public ClassOverview(Type type, IEnumerable<IPropertyOverview> properties, IClassReflectionOperations operations)
        {
            Type = type;
            _operations = operations;
            Name = type.Name;
            IsSubClass = _operations.IsSubClass(type);
            var displayAttribute = _operations.GetClassDisplayNames(type);
            DisplayNameSingular = displayAttribute.Singular;
            DisplayNamePlural = displayAttribute.Plural;
            Properties = properties.ToList();
            UserClassProperties = properties.Where(p => p.IsTypeOfUserClass).ToList();
            UserClassNotCollectionProperties = UserClassProperties.Where(p => !p.IsCollection).ToList();
            UserClassCollectionProperties = UserClassProperties.Where(p => p.IsCollection).ToList();
        }
    }
}
