using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration.Ini;
using Nacos.Config;

namespace NacosExtensions.Configuration
{
    public class IniConfigurationParser : INacosConfigurationParser
    {
        public IDictionary<string, string> Parse(string input)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            writer.Write(input);
            writer.Flush();
            stream.Position = 0;

            return IniStreamConfigurationProvider.Read(stream);
        }
    }
}