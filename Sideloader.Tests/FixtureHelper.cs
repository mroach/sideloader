using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sideloader.Tests
{
    public class FixtureHelper
    {
        public static string RootPath()
        {
            var root = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            return Path.Combine(root, "Fixtures");
        }

        public static string FixturePath(params string[] path)
        {
            return Path.Combine(RootPath(), Path.Combine(path));
        }
    }
}
