# 指定 Internal 成员对外部程序集可见

```c#
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("your assemblyName")]
```

## Examples

新建类库项目`ProjectA`，并新建`Dashboard`类

```c#
namespace ProjectA
{
    public class Dashboard
    {
        public string Title { get; internal set; }

        internal void SetTitle(string title)
        {
            Title = title;
        }
    }
}
```

在`ProjectA`项目的根目录下新建`AssemblyInfo.cs`文件

```c#
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProjectATest")]
```

新建单元测试项目`ProjectATest`，并新建`DashboardTests`类

```c#
using ProjectA;
using Xunit;

namespace ProjectATest
{
    public class DashboardTests
    {
        [Fact]
        public void Test1()
        {
            Dashboard dashboard = new();

            dashboard.Title = "Test";

            Assert.Equal("Test", dashboard.Title);
        }

        [Fact]
        public void Test2()
        {
            Dashboard dashboard = new();

            dashboard.SetTitle("Test");

            Assert.Equal("Test", dashboard.Title);
        }
    }
}
```
