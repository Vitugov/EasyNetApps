using System.Reflection;

namespace EasyNetApps.Core.Reflection.ReflectionOperations
{
    public interface IPropertyReflectionOperations
    {
        Type GetGenericOfIEnumerableExceptChars(PropertyInfo property);
        string GetPropertyDisplayName(PropertyInfo property);
        bool IsCollection(PropertyInfo property);
        bool IsVisible(PropertyInfo property);
    }
}