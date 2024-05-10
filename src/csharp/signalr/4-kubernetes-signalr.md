# Kubernetes 微服务架构

当应用在 Kubernetes 上运行多个 Pod 时，Ingress 通常会平衡每个 Pod 之间的请求，以保持流量负载均衡，因此，顺序请求很可能会发送到不同的 Pod。

当连接请求发送到与协商请求不同的 Pod 时，服务器会返回 `404 Not Found` 并且 SignalR 连接失败。

## 跳过协商

将客户端设置为强制使用 WebSockets 传输，同时设置跳过协商步骤。这意味着一旦建立连接后，客户端将在 SignalR 连接的整个生命周期内保持连接并与同一个 Pod 通信。由于不再发送 HTTP 请求，因此不存在将后续请求路由发送到不同 Pod 的风险。

WebSockets 是唯一可以跳过协商的传输，因为它寿命很长并且不需要连接 ID。

下面是 JavaScript 的示例代码：

```jsx
let connection = new signalR.HubConnectionBuilder()
    .withUrl("<https://example.com/myHub>", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    })
    .build();
```

下面是 C# 的示例代码：

```csharp
var connection = new HubConnectionBuilder()
    .WithUrl("<https://example.com/myHub>", options => {
        options.SkipNegotiation = true;
        options.Transport = SignalR.HttpTransportType.WebSockets;
    })
    .Build();
```

## 使用 Redis

使用 Redis 允许 SignalR 在服务的不同实例之间通信，以确保每个实例都能向所有客户端发送消息，而不仅仅是直接连接到它的客户端。

安装依赖包：

```bash
Install-Package Microsoft.AspNetCore.SignalR.StackExchangeRedis
```

修改服务端代码，添加 Redis 的支持：

```csharp
services.AddSignalR().AddStackExchangeRedis("<http://127.0.0.1:6379>");
```

## 参考

- [SignalR on Kubernetes - G Research](https://www.gresearch.co.uk/blog/article/signalr-on-kubernetes/)
