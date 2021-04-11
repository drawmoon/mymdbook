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
