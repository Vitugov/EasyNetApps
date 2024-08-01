using System.Reflection;

namespace EasyNetApps.Core.Reflection.Properties
{
    public interface IPropertyOverview
    {
        PropertyInfo Property { get; }
        string Name { get; }
        string DisplayName { get; }
        bool IsCollection { get; }
        bool IsGenericType { get; }
        Type? GenericOfIEnumerable { get; }
        bool IsTypeOfUserClass { get; }
        bool IsVisible { get; }
    }
}