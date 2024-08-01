using EasyNetApps.Core.Reflection.UserEntityInterface;

namespace EasyNetApps.DbAccess.Old
{
    public class SubCollectionSavePreparer<T>(IEnumerable<T> edit, IEnumerable<T> origin)
        where T : class, IProjectModel
    {
        public List<T> ToAdd { get; set; } = edit.Where(item => !origin.Contains(item)).ToList();
        public List<T> ToUpdate { get; set; } = origin.Where(item => edit.Contains(item)).ToList();
        public List<T> ToDelete { get; set; } = origin.Where(item => !edit.Contains(item)).ToList();

        //public void Save() => DbHandler.SaveSubCollection<T>(ToAdd, ToUpdate, ToDelete);
    }
}
