using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sideloader.FlightSim
{
    /// <summary>
    /// Represents the standard manifest file data.
    /// 
    /// This file format is from Flight Simulator itself.
    /// </summary>
    public class Manifest
    {
        public string ContentType { get; set; }

        public string Title { get; set; }

        public string Manufacturer { get; set; }

        public string Creator { get; set; }

        public Version PackageVersion { get; set; }

        public Version MinimumGameVersion { get; set; }

        public static Manifest LoadFromFile(string path)
        {
            return LoadFromString(File.ReadAllText(path));
        }

        public static Manifest LoadFromString(string json)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            return JsonConvert.DeserializeObject<Manifest>(json, settings);
        }
    }
}
