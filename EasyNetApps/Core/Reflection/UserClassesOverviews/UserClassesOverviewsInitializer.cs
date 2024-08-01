using EasyNetApps.Core.Reflection.ClassOverview;
using EasyNetApps.Core.Reflection.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasyNetApps.Core.Reflection.Delegates.Delegates;

namespace EasyNetApps.Core.Reflection.UserClassesOverviews
{
    public class UserClassesOverviewsInitializer(
        IUserClasses userClasses,
        IClassOverviewFactory classOverviewFactory,
        UserClassesOverviewsCreator userClassesOverviewsCreator) : IUserClassesOverviewsInitializer
    {
        private readonly IUserClasses _userClasses = userClasses;
        private readonly IClassOverviewFactory _classOverviewFactory = classOverviewFactory;
        private readonly UserClassesOverviewsCreator _userClassesOverviewsCreator = userClassesOverviewsCreator;

        public IUserClassesOverviews Initialize()
        {
            var classOverviews = _userClasses.Items.Select(userClass => _classOverviewFactory.Create(userClass)).ToList();
            return _userClassesOverviewsCreator(_userClasses, classOverviews);
        }
    }
}
