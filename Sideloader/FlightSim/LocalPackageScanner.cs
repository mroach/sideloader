using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sideloader.FlightSim
{
    public class LocalPackageScanner
    {
        public string BasePath { get; private set; }

        public const string ManifestFileName = "manifest.json";

        public static string ApplicationId { get; }
            = "Microsoft.FlightSimulator_8wekyb3d8bbwe";

        public static string DefaultAppDataPath()
            => Environment.GetEnvironmentVariable("LOCALAPPDATA");

        public LocalPackageScanner() : this(DefaultAppDataPath())
        {
        }

        public LocalPackageScanner(string basePath)
        {
            BasePath = basePath;
        }

        public IEnumerable<InstalledPackage> GetAllInstalled()
        {
            return (
                from dir in GetCommunityPackageDirs()
                let manifestPath = Path.Combine(dir, ManifestFileName)
                select new InstalledPackage
                {
                    FullPath = dir,
                    Manifest = Manifest.LoadFromFile(manifestPath)
                }
            ).ToArray();
        }

        /// <summary>
        /// Get a list of directories containing a manifest.json file
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public IEnumerable<string> GetCommunityPackageDirs()
        {
            return (
                from dir in Directory.GetDirectories(GetCommunityPath())
                let manifestPath = Path.Combine(dir, ManifestFileName)
                where File.Exists(manifestPath)
                select dir
            ).ToArray();
        }

        public string GetCommunityPath()
        {
            return Path.Combine(
                BasePath,
                "Packages",
                ApplicationId,
                "LocalCache",
                "Packages",
                "Community"
            );
        }
    }
}
