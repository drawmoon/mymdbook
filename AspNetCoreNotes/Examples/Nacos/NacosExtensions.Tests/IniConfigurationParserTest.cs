using System;
using NacosExtensions.Configuration;
using Xunit;

namespace NacosExtensions.Tests
{
    public class IniConfigurationParserTest
    {
        [Fact]
        public void Test1()
        {
            var txt = @"
version = ""测试version""

[ConnectionStrings]
Default = ""Server=127.0.0.1;Port=3306;Database=demo;User Id=root;Password=123456;""

[AppSettings]
Str = ""val""
num = 1

[AppSettings:arr]
0 = 1
1 = 2
2 = 3

[AppSettings:subobj]
a = ""b""
";

            var data = new IniConfigurationParser().Parse(txt);
            
            /*
             *    [version, 测试version]
             *    [ConnectionStrings:Default, Server=127.0.0.1;Port=3306;Database=demo;User Id=root;Password=123456;]
             *    [AppSettings:Str, val]
             *    [AppSettings:num, 1]
             *    [AppSettings:arr:0, 1]
             *    [AppSettings:arr:1, 2]
             *    [AppSettings:arr:2, 3]
             *    [AppSettings:subobj:a, b]
             */
            
            Assert.NotNull(data);
        }
    }
}