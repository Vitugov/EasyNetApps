using EasyNetApps.Core.Attributes;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EasyNetApps.Core.Reflection.UserEntityInterface
{
    public abstract partial class ProjectModel : IProjectModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Key]
        [DisplayName("Id")]
        [Invisible]
        public Guid Id { get; set; } = Guid.NewGuid();

        private string displayName = "";
        [Invisible]
        [DisplayName("Display name")]
        public string DisplayName
        {
            get => displayName;
            set
            {
                if (value != displayName)
                {
                    displayName = value;
                    OnPropertyChanged();
                }
            }
        }
        public ProjectModel() { }
        public virtual void UpdateFrom(IProjectModel obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                throw new ArgumentException("Object must be of the same type.", nameof(obj));
            }

            var properties = GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    var value = prop.GetValue(obj);
                    prop.SetValue(this, value);
                    OnPropertyChanged(prop.Name);
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName != nameof(DisplayName))
            {
                UpdateDisplayName();
            }
        }

        protected abstract void UpdateDisplayName();

        public override string ToString() => DisplayName;

        public virtual object Clone()
        {
            var clone = (IProjectModel)Activator.CreateInstance(GetType());

            foreach (var property in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = property.GetValue(this);

                // Проверяем, является ли значение коллекцией
                if (property.PropertyType.IsAssignableTo(typeof(IEnumerable)) && property.PropertyType != typeof(string))
                {
                    var genericType = GetGenericOfIEnumerableExceptChars(property);
                    //var genericType = classOverview.CollectionGenericParameter;

                    if (typeof(ICloneable).IsAssignableFrom(genericType))
                    {
                        // Создаем новую коллекцию и клонируем каждый элемент
                        var clonedList = (IList)Activator.CreateInstance(value.GetType())!;
                        foreach (var item in (IEnumerable)value)
                        {
                            clonedList.Add(((ICloneable)item).Clone());
                        }
                        property.SetValue(clone, clonedList);
                    }
                    else
                    {
                        // Просто копируем коллекцию, если элементы не реализуют ICloneable
                        property.SetValue(clone, value);
                        throw new Exception("Collection should be from ICloneable objects");
                    }
                }
                else
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(clone, value);
                    }
                }
                clone.OnPropertyChanged(property.Name);
            }
            return clone;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            ProjectModel other = (ProjectModel)obj;
            return Id == other.Id; // Сравниваем только по GUID
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(ProjectModel me, ProjectModel other)
        {
            return Equals(me, other);
        }

        public static bool operator !=(ProjectModel me, ProjectModel other)
        {
            return !Equals(me, other);
        }

        private Type GetGenericOfIEnumerableExceptChars(PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            Type elementType = propertyType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                    && i.GetGenericTypeDefinition() != typeof(IEnumerable<char>))
                .Select(i => i.GetGenericArguments()[0])
                .First();
            return elementType;
        }
    }
}
