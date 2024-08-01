using EasyNetApps.Core.Attributes;
using EasyNetApps.Core.Reflection.UserClasses;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace EasyNetApps.Core.Reflection.Properties
{
    public class PropertyOverview : IPropertyOverview
    {
        public PropertyInfo Property { get; set; }
        public string DisplayName { get; set; }
        public bool IsCollection { get; set; }
        public bool IsGenericType { get; set; }
        public Type? GenericOfIEnumerable { get; set; }
        public bool IsTypeOfUserClass { get; set; }
        public bool IsVisible { get; set; }

        public PropertyOverview(PropertyInfo property, IUserClasses userClasses)
        {
            Property = property;
            DisplayName = property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name;
            IsCollection = property.PropertyType.IsAssignableTo(typeof(IEnumerable)) && property.PropertyType != typeof(string);
            IsGenericType = property.PropertyType.IsGenericType;
            GenericOfIEnumerable = IsGenericType ? GetGenericOfIEnumerableExceptChars(property) : null;
            IsTypeOfUserClass = IsCollection ? userClasses.Contains(GenericOfIEnumerable!) : userClasses.Contains(property.PropertyType);
            IsVisible = property.GetCustomAttribute<InvisibleAttribute>() == null && property.GetGetMethod(false) != null;
        }

        private static Type GetGenericOfIEnumerableExceptChars(PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            Type elementType = propertyType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                    && i.GetGenericTypeDefinition() != typeof(IEnumerable<char>))
                .Select(i => i.GetGenericArguments()[0])
                .First();
            return elementType;
        }
    }
}
