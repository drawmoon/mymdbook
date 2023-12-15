using HttpClientDiagnostics;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler()));

var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://www.example.com");

httpClient.SendAsync(requestMessage).ConfigureAwait(false).GetAwaiter().GetResult();