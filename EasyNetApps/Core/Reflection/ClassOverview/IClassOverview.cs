using EasyNetApps.Core.Reflection.Properties;

namespace EasyNetApps.Core.Reflection.ClassOverview
{
    public interface IClassOverview
    {
        public Type Type { get; }
        public string Name { get; set; }
        public string DisplayNameSingular { get; set; }
        public string DisplayNamePlural { get; set; }
        public bool IsSubClass { get; }
        public bool ShowInMainWindow { get; }
        public List<IPropertyOverview> Properties { get; }
    }
}