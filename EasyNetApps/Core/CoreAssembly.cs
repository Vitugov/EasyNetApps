using EasyNetApps.Core.Reflection.ClassOverview;
using EasyNetApps.Core.Reflection.Properties;
using EasyNetApps.Core.Reflection.PropertyOverview;
using EasyNetApps.Core.Reflection.UserClasses;
using EasyNetApps.Core.Reflection.UserClassesOverviews;
using EasyNetApps.Core.Reflection.UserEntityInterface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using static EasyNetApps.Core.Reflection.Delegates.Delegates;

namespace EasyNetApps.Core
{
    public class CoreAssembly
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserClasses>(provider => new UserClasses(typeof(IProjectModel)));
            
            services.AddTransient<IPropertyOverviewFactory, PropertyOverviewFactory>();
            services.AddTransient<IClassOverviewFactory, ClassOverviewFactory>();

            services.AddTransient<PropertyOverviewCreator>(provider => (p, uс) => new PropertyOverview(p, uс));
            services.AddTransient<ClassOverviewCreator>(provider => (uc, pc) => new ClassOverview(uc, pc));
            services.AddTransient<UserClassesOverviewsCreator>(provider => (uc, co) => new UserClassesOverviews(uc, co));

            services.AddSingleton<IUserClassesOverviews>(provider =>
                provider.GetRequiredService<UserClassesOverviewsInitializer>().Initialize());
        }
    }
}
