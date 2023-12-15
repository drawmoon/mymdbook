using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using WebApplication1.Test;

namespace WebApplication1.IntegrationTest.Base
{
    public class TestFixture : IDisposable
    {
        public TestServer TestServer;

        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseKestrel(options => options.Listen(IPAddress.Parse("0.0.0.0"), 3000))
                .UseStartup<StartupTest>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}