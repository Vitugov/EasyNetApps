using EasyNetApps.Core.Reflection.ClassOverview;
using EasyNetApps.Core.Reflection.Properties;
using EasyNetApps.Core.Reflection.PropertyOverview;
using EasyNetApps.Core.Reflection.ReflectionOperations;
using EasyNetApps.Core.Reflection.UserClasses;
using EasyNetApps.Core.Reflection.UserClassesOverviews;
using EasyNetApps.Core.Reflection.UserEntityInterface;
using Microsoft.Extensions.DependencyInjection;
using static EasyNetApps.Core.Reflection.Delegates.Delegates;

namespace EasyNetApps.Core
{
    public class CoreAssembly
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserClasses>(provider => new UserClasses(typeof(IProjectModel)));

            services.AddSingleton<IUserClassesOverviewsInitializer, UserClassesOverviewsInitializer>();
            services.AddSingleton<IClassOverviewFactory, ClassOverviewFactory>();
            services.AddSingleton<IPropertyOverviewFactory, PropertyOverviewFactory>();
            services.AddSingleton<IClassReflectionOperations, ClassReflectionOperations>();
            services.AddSingleton<IPropertyReflectionOperations, PropertyReflectionOperations>();

            services.AddSingleton<UserClassesOverviewsCreator>(provider => (co) => new UserClassesOverviews(co));
            services.AddSingleton<ClassOverviewCreator>(provider => (uc, pc, o) => new ClassOverview(uc, pc, o));
            services.AddSingleton<PropertyOverviewCreator>(provider => (p, uс, o) => new PropertyOverview(p, uс, o));

            services.AddSingleton<IUserClassesOverviews>(provider =>
                provider.GetRequiredService<IUserClassesOverviewsInitializer>().Initialize());
        }
    }
}
