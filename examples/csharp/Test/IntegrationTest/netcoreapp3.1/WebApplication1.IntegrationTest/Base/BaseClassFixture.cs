using System.Net.Http;
using Xunit;

namespace WebApplication1.IntegrationTest.Base
{
    public class BaseClassFixture : IClassFixture<TestFixture>
    {
        protected readonly HttpClient Client;

        public BaseClassFixture(TestFixture fixture)
        {
            Client = fixture.Client;
        }
    }
}