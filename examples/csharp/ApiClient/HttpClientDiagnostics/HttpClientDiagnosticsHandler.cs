using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDiagnostics
{
    public class HttpClientDiagnosticsHandler : DelegatingHandler
    {
        public HttpClientDiagnosticsHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Log.Debug("开始处理 HTTP 请求: {0}", request.RequestUri.AbsolutePath);

            var totalElapsedTime = Stopwatch.StartNew();

            Log.Debug("HTTP 请求消息: {0}", request);
            if (request.Content != null)
            {
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Log.Debug("HTTP 请求消息的内容: {0}", content);
            }

            var responseElapsedTime = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken);

            Log.Debug("HTTP 响应消息: {0}", response);
            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Log.Debug("HTTP 响应消息的内容: {0}", content);
            }

            responseElapsedTime.Stop();
            Log.Debug("响应耗时: {0} ms", responseElapsedTime.ElapsedMilliseconds);

            totalElapsedTime.Stop();
            Log.Debug("结束处理 HTTP 请求, 响应状态码: {0}. 总共耗时: {1} ms", response.StatusCode,
                totalElapsedTime.ElapsedMilliseconds);

            return response;
        }
    }
}
