using EasyNetApps.Core.Attributes;
using System.Reflection;

namespace EasyNetApps.Core.Reflection.ReflectionOperations
{
    public class ClassReflectionOperations : IClassReflectionOperations
    {
        public DisplayNamesAttribute GetClassDisplayNames(Type type) =>
            type.GetCustomAttribute<DisplayNamesAttribute>() ?? new DisplayNamesAttribute(type.Name, type.Name);
        public bool IsSubClass(Type type) => type.GetCustomAttribute<SubClassAttribute>() != null;
    }
}
