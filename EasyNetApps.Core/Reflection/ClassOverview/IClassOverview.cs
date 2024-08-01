using EasyNetApps.Core.Reflection.Properties;

namespace EasyNetApps.Core.Reflection.ClassOverview
{
    public interface IClassOverview
    {
        Type Type { get; }
        string Name { get; }
        string DisplayNameSingular { get; }
        string DisplayNamePlural { get; }
        bool IsSubClass { get; }
        bool ShowInMainWindow { get; }
        List<IPropertyOverview> Properties { get; }
        List<IPropertyOverview> UserClassProperties { get; }
        List<IPropertyOverview> UserClassNotCollectionProperties { get; }
        List<IPropertyOverview> UserClassCollectionProperties { get; }
        List<IPropertyOverview> GetPropertiesOfType(Type type);
    }
}