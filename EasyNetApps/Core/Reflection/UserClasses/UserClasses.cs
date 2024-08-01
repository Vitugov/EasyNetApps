using System.Reflection;

namespace EasyNetApps.Core.Reflection.UserClasses
{
    public class UserClasses : IUserClasses
    {
        private readonly Type _globalUserTypeInterface;
        public List<Type> Items { get; }

        public UserClasses(Type globalUserTypeInterface)
        {
            _globalUserTypeInterface = globalUserTypeInterface;
            Items = GetAllUserClasses(globalUserTypeInterface);
        }

        public bool Contains(Type type) => Items.Contains(type);


        private static List<Type> GetAllUserClasses(Type globalUserTypeInterface)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var allDerivedTypes = assemblies.SelectMany(assembly =>
            {
                try
                {
                    return assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(globalUserTypeInterface));
                }
                catch (ReflectionTypeLoadException ex)
                {
                    return ex.Types
                        .Where(t => t != null && t.IsClass && !t.IsAbstract && t.IsSubclassOf(globalUserTypeInterface));
                }
            }).ToList();

            if (allDerivedTypes == null || allDerivedTypes.Count != 0)
            {
                throw new Exception($"There is no classes dervived from {globalUserTypeInterface.Name}.");
            }

            return allDerivedTypes!;
        }


    }
}
