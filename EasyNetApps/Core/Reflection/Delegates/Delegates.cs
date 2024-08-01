using EasyNetApps.Core.Reflection.ClassOverview;
using EasyNetApps.Core.Reflection.Properties;
using EasyNetApps.Core.Reflection.UserClasses;
using EasyNetApps.Core.Reflection.UserClassesOverviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetApps.Core.Reflection.Delegates
{
    public class Delegates
    {
        public delegate IUserClassesOverviews UserClassesOverviewsCreator(IUserClasses userClasses, IEnumerable<IClassOverview> classOverviews);
        public delegate IClassOverview ClassOverviewCreator(Type userClass, IEnumerable<IPropertyOverview> propertyOverviews);
        public delegate IPropertyOverview PropertyOverviewCreator(PropertyInfo property, IUserClasses userClasses);
    }
}
