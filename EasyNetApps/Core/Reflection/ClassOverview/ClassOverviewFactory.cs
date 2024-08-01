using EasyNetApps.Core.Reflection.PropertyOverview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static EasyNetApps.Core.Reflection.Delegates.Delegates;

namespace EasyNetApps.Core.Reflection.ClassOverview
{
    public class ClassOverviewFactory(IPropertyOverviewFactory propertyOverviewFactory, ClassOverviewCreator classOverviewCreator)
        : IClassOverviewFactory
    {
        private readonly IPropertyOverviewFactory _propertyOverviewFactory = propertyOverviewFactory;
        private readonly ClassOverviewCreator _classOverviewCreator = classOverviewCreator;

        public IClassOverview Create(Type userClass)
        {
            var properties = userClass.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var propertyOverviews = properties.Select(property => _propertyOverviewFactory.Create(property)).ToList();
            return _classOverviewCreator(userClass, propertyOverviews);
        }
    }
}
