using EasyNetApps.Core.Reflection.UserEntityInterface;
using System.Collections;
using System.Collections.ObjectModel;

namespace EasyNetApps.DbActions
{
    public class SubCollectionSaver<T>
        where T : ProjectModel
    {
        public List<T> ToAdd { get; set; }
        public List<T> ToUpdate { get; set; }
        public List<T> ToDelete { get; set; }

        public SubCollectionSaver(IList editCollection, IList originColection)
        {
            var _editCollection = (ObservableCollection<T>)editCollection;
            var _originCollection = (ObservableCollection<T>)originColection;
            ToAdd = _editCollection
                .Where(item => !_originCollection.Contains(item))
                .ToList();
            ToUpdate = _originCollection
                .Where(item => _editCollection.Contains(item))
                .ToList();
            ToDelete = _originCollection
                .Where(item => !_editCollection.Contains(item))
            .ToList();
            Save();
        }

        public void Save() => DbHandler.SaveSubCollection<T>(ToAdd, ToUpdate, ToDelete);
    }
}
