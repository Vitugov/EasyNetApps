using EasyNetApps.Core.Reflection.Properties;
using System.Reflection;

namespace EasyNetApps.Core.Reflection.PropertyOverview
{
    public interface IPropertyOverviewFactory
    {
        IPropertyOverview Create(PropertyInfo property);
    }
}