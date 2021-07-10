using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhysicalFileProviderDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Holle World");

            while (true)
            {
                MainAsync().GetAwaiter().GetResult();
            }
        }

        private static async Task MainAsync()
        {
            var provider = new PhysicalFileProvider(Environment.CurrentDirectory);

            //ChangeToken.OnChange(
            //  () => provider.Watch("appsettings.json"),
            //  () =>
            //  {

            //  });

            var token = provider.Watch("appsettings.json");

            var tcs = new TaskCompletionSource<object>();

            token.RegisterChangeCallback(state => ((TaskCompletionSource<object>)state).SetResult(null), tcs);

            await tcs.Task.ConfigureAwait(false);

            var obj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));

            foreach (var (key, value) in obj)
            {
                Console.WriteLine($"{key}: {value}");
            }
        }
    }
}
