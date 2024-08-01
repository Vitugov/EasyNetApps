namespace EasyNetApps.Core.Reflection.UserClasses
{
    public interface IUserClasses
    {
        List<Type> Items { get; }
        bool Contains(Type type);
    }
}