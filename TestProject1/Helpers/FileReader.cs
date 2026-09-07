using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using NUnit.Framework;

namespace Tests1.Helpers
{
    public class FileReader
    {
        public static T ReadJson<T>(string fileName)
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", fileName);
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}