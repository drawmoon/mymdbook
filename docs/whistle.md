# Web 调试代理工具

> Whistle 是基于 Node 实现的跨平台 Web 调试代理工具，主要用于查看、修改 HTTP、HTTPS、Websocket 的请求、响应，也可以作为 HTTP 代理服务器使用

安装 Whistle

```bash
npm install -g whistle
```

安装浏览器插件 Proxy SwitchyOmega，将请求代理到 Whistle；也可以使用浏览器或系统代理

新建情景模式 > 选择代理服务器 > 创建

代理协议选择 HTTP，代理服务器填写 127.0.0.1，代理端口填写 8899，然后应用选项，点击浏览器插件上的 SwitchyOmega，选择刚刚创建的代理情景

启动 Whistle

```bash
w2 start
```

在浏览器访问`http://local.whistlejs.com/`开始使用 Whistle

## 配置一个简单的 Http 代理

```conf
http://report.net/api/1/file http://localhost:3000/api/1/file

http://report.net http://localhost:5000
```
