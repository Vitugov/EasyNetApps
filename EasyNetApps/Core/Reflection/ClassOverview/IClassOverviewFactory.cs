namespace EasyNetApps.Core.Reflection.ClassOverview
{
    public interface IClassOverviewFactory
    {
        IClassOverview Create(Type userClass);
    }
}