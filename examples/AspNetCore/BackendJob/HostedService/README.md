# 排队的后台任务

实现`IBackgroundTask`接口，并注入到容器中，当程序启动时，会在容器中获取`IBackgroundTask`的实现，并添加到后台任务的队列中。
