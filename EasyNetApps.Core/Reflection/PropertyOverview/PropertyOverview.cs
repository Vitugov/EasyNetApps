using EasyNetApps.Core.Reflection.ReflectionOperations;
using EasyNetApps.Core.Reflection.UserClasses;
using System.Reflection;

namespace EasyNetApps.Core.Reflection.Properties
{
    public class PropertyOverview : IPropertyOverview
    {
        private readonly IPropertyReflectionOperations _operations;

        public PropertyInfo Property { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public bool IsCollection { get; }
        public bool IsGenericType { get; }
        public Type? GenericOfIEnumerable { get; }
        public bool IsTypeOfUserClass { get; }
        public bool IsVisible { get; }

        public PropertyOverview(PropertyInfo property, IUserClasses userClasses, IPropertyReflectionOperations operations)
        {
            Property = property;
            _operations = operations;
            Name = property.Name;
            DisplayName = _operations.GetPropertyDisplayName(property);
            IsCollection = _operations.IsCollection(property);
            IsGenericType = property.PropertyType.IsGenericType;
            GenericOfIEnumerable = IsGenericType ? _operations.GetGenericOfIEnumerableExceptChars(property) : null;
            IsTypeOfUserClass = IsCollection ? userClasses.Contains(GenericOfIEnumerable!) : userClasses.Contains(property.PropertyType);
            IsVisible = _operations.IsVisible(property);
        }
    }
}
