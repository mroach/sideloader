using System;
using System.IO;
using Xunit;

namespace Sideloader.Tests.FlightSim
{
    public class ManifestTests
    {
        [Fact]
        public void LoadFromString_WithValidManifest_Deserializes()
        {
            var json = File.ReadAllText(FixtureHelper.FixturePath("Manifests", "livery.json"));
            var result = Sideloader.FlightSim.Manifest.LoadFromString(json);

            Assert.Equal("Open Livery Studios", result.Creator);
            Assert.Equal("AIRCRAFT", result.ContentType);
            Assert.Equal("A320 Neo - Lufthansa", result.Title);
            Assert.Equal(Version.Parse("2.0.1"), result.PackageVersion);
            Assert.Equal(Version.Parse("1.7.12"), result.MinimumGameVersion);
            Assert.Equal("Airbus", result.Manufacturer);
        }
    }
}
