using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sideloader
{
    /// <summary>
    /// Simple in-memory registry of add-ins.
    /// 
    /// Should be replaced by something persisted to disk, but the interface can likely remain the same.
    /// </summary>
    public class PackageRegistry
    {
        private ObservableCollection<Package> Items { get; set; }
            = new ObservableCollection<Package>();

        private PackageRegistry() { }

        private static IDictionary<string, PackageRegistry> _instances
            = new Dictionary<string, PackageRegistry>();

        public static PackageRegistry GetInstance(string type)
        {
            if (!_instances.ContainsKey(type))
            {
                _instances[type] = new PackageRegistry();
            }
            return _instances[type];
        }

        public Package GetByUuid(string uuid)
        {
            return Items.FirstOrDefault(p => p.Uuid == uuid);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public void Add(Package package)
        {
            Items.Add(package);
        }

        public void AddMany(IEnumerable<Package> packages)
        {
            foreach (var package in packages)
            {
                Add(package);
            }
        }

        public ObservableCollection<Package> All()
        {
            return Items;
        }
    }
}
