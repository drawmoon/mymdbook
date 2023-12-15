using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using WebApplication1.IntegrationTest.Base;
using Xunit;

namespace WebApplication1.IntegrationTest
{
    public class SayHiControllerTest : BaseClassFixture
    {
        public SayHiControllerTest(TestFixture fixture) : base(fixture)
        {
        }
        
        [Fact]
        public async Task Get()
        {
            var response = await Client.GetAsync("api/SayHi");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsStringAsync()).Should().Be("Hello, World!");
        }
    }
}