using System.Reflection;

namespace EasyNetApps.Core.Reflection.Properties
{
    public interface IPropertyOverview
    {
        PropertyInfo Property { get; set; }
        string DisplayName { get; set; }
        bool IsCollection { get; set; }
        bool IsGenericType { get; set; }
        Type? GenericOfIEnumerable { get; set; }
        bool IsTypeOfUserClass { get; set; }
        bool IsVisible { get; set; }
    }
}