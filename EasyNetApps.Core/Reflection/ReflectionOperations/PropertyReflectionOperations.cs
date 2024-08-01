using EasyNetApps.Core.Attributes;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace EasyNetApps.Core.Reflection.ReflectionOperations
{
    public class PropertyReflectionOperations : IPropertyReflectionOperations
    {
        public string GetPropertyDisplayName(PropertyInfo property) =>
            property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name;

        public bool IsCollection(PropertyInfo property) =>
            property.PropertyType.IsAssignableTo(typeof(IEnumerable)) && property.PropertyType != typeof(string);

        public bool IsVisible(PropertyInfo property) =>
            property.GetCustomAttribute<InvisibleAttribute>() == null && property.GetGetMethod(false) != null;

        public Type GetGenericOfIEnumerableExceptChars(PropertyInfo property)
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
