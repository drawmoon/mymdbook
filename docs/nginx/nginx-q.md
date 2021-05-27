# Nginx 常见问题

## [emerg]: unknown directive in xxx

`nginx.conf`文件编码问题导致，更改为`UTF-8`即可

## The character [_] is never valid in a domain name

```txt
HTTP Status 400 – Bad Request
Type Exception Report

Message The character [_] is never valid in a domain name.

Description The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).

Exception

java.lang.IllegalArgumentException: The character [_] is never valid in a domain name.
  org.apache.tomcat.util.http.parser.HttpParser$DomainParseState.next(HttpParser.java:983)
  org.apache.tomcat.util.http.parser.HttpParser.readHostDomainName(HttpParser.java:879)
  org.apache.tomcat.util.http.parser.Host.parse(Host.java:66)
  org.apache.tomcat.util.http.parser.Host.parse(Host.java:40)
  org.apache.coyote.AbstractProcessor.parseHost(AbstractProcessor.java:293)
  org.apache.coyote.http11.Http11Processor.prepareRequest(Http11Processor.java:1198)
  org.apache.coyote.http11.Http11Processor.service(Http11Processor.java:775)
  org.apache.coyote.AbstractProcessorLight.process(AbstractProcessorLight.java:66)
  org.apache.coyote.AbstractProtocol$ConnectionHandler.process(AbstractProtocol.java:810)
  org.apache.tomcat.util.net.NioEndpoint$SocketProcessor.doRun(NioEndpoint.java:1498)
  org.apache.tomcat.util.net.SocketProcessorBase.run(SocketProcessorBase.java:49)
  java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1142)
  java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:617)
  org.apache.tomcat.util.threads.TaskThread$WrappingRunnable.run(TaskThread.java:61)
  java.lang.Thread.run(Thread.java:745)
Note The full stack trace of the root cause is available in the server logs.

Apache Tomcat/8.5.45
```

Tomcat 的 Domain name 中不支持有下划线，将下划线去掉即可

```conf
location /app {
    # proxy_pass http://app_service:8080/api;
    proxy_pass http://appservice:8080/api;
}
```
