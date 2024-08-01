using EasyNetApps.Core.Attributes;
using EasyNetApps.Core.Reflection.Properties;
using System.Reflection;

namespace EasyNetApps.Core.Reflection.ClassOverview
{
    public class ClassOverview : IClassOverview
    {
        public Type Type { get; }
        public string Name { get; set; }
        public string DisplayNameSingular { get; set; }
        public string DisplayNamePlural { get; set; }
        public bool IsSubClass { get; }

        public bool ShowInMainWindow { get; }
        public List<IPropertyOverview> Properties { get; }

        public ClassOverview(Type type, IEnumerable<IPropertyOverview> properties)
        {
            Type = type;
            Name = type.Name;
            IsSubClass = type.GetCustomAttribute<SubClassAttribute>() != null;
            var displayAttribute = Type.GetCustomAttribute<DisplayNamesAttribute>() ?? new DisplayNamesAttribute(Type.Name, Type.Name);
            DisplayNameSingular = displayAttribute.Singular;
            DisplayNamePlural = displayAttribute.Plural;
            Properties = properties.ToList();
        }
    }
}
