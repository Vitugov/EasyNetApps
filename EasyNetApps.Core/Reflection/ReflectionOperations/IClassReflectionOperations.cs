using EasyNetApps.Core.Attributes;

namespace EasyNetApps.Core.Reflection.ReflectionOperations
{
    public interface IClassReflectionOperations
    {
        DisplayNamesAttribute GetClassDisplayNames(Type type);
        bool IsSubClass(Type type);
    }
}