using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


while (true)
{
    Monitor().GetAwaiter().GetResult();
}

static async Task Monitor()
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

    var obj = JsonConvert.DeserializeObject<JObject>(await File.ReadAllTextAsync("./appsettings.json"));

    foreach (var (key, value) in obj)
    {
        Console.WriteLine($"{key}: {value}");
    }
}