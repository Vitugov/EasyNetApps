using EasyNetApps.Core.Reflection.Properties;
using EasyNetApps.Core.Reflection.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static EasyNetApps.Core.Reflection.Delegates.Delegates;

namespace EasyNetApps.Core.Reflection.PropertyOverview
{

    public class PropertyOverviewFactory(PropertyOverviewCreator propertyOverviewCreator, IUserClasses userClasses)
        : IPropertyOverviewFactory
    {
        private readonly PropertyOverviewCreator _propertyOverviewCreator = propertyOverviewCreator;
        private readonly IUserClasses _userClasses = userClasses;

        public IPropertyOverview Create(PropertyInfo property)
        {
            return _propertyOverviewCreator(property, _userClasses);
        }
    }
}
