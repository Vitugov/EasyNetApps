using System.ComponentModel;

namespace EasyNetApps.Core.Reflection.UserEntityInterface
{
    public interface IProjectModel : ICloneable, INotifyPropertyChanged
    {
        string DisplayName { get; set; }
        Guid Id { get; set; }

        bool Equals(object? obj);
        int GetHashCode();
        string ToString();
        void UpdateFrom(ProjectModel obj);
    }
}