using System.IO;

namespace Sideloader.FlightSim
{
    public class InstalledPackage
    {
        public string FullPath { get; set; }

        public string DirectoryName
        {
            get { return Path.GetDirectoryName(FullPath); }
        }

        public Manifest Manifest { get; set; }
    }
}
