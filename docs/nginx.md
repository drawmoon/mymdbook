# Nginx

## 安装 Nginx

安装依赖包

```bash
sudo apt install gcc libpcre3-dev zlib1g-dev openssl libssl-dev make
```

- `gcc`: C 语言编译器
- `libpcre3-dev`: 正则表达式库
- `zlib1g-dev`: 数据压缩库
- `openssl`: TLS/SSL 加密库
- `make`: 解释 Makefile 的命令工具

下载并解压安装包

```bash
wget https://nginx.org/download/nginx-1.18.0.tar.gz
tar -zxvf nginx-1.18.0.tar.gz
```

执行 Nginx 默认配置

```bash
cd nginx-1.18.0/
./configure
```

执行编译、安装

```bash
make & make install
```

将 Nginx 添加到全局变量中

```bash
ln -s /usr/local/nginx/sbin/nginx /usr/local/bin/
```

启动 Nginx

```bash
nginx
```

## 简单的代理配置

```conf

#user  nobody;
worker_processes  1;

#error_log  logs/error.log;
#error_log  logs/error.log  notice;
#error_log  logs/error.log  info;

#pid        logs/nginx.pid;


events {
    worker_connections  1024;
}


http {
    include       mime.types;
    default_type  application/octet-stream;

    #log_format  main  '$remote_addr - $remote_user [$time_local] "$request" '
    #                  '$status $body_bytes_sent "$http_referer" '
    #                  '"$http_user_agent" "$http_x_forwarded_for"';

    #access_log  logs/access.log  main;

    sendfile        on;
    #tcp_nopush     on;

    #keepalive_timeout  0;
    keepalive_timeout  65;

    #gzip  on;

    server {
        listen       80;
        server_name  localhost;

        #charset koi8-r;

        #access_log  logs/host.access.log  main;

        location /api/fs {
            proxy_pass  http://127.0.0.1:3000/api;
        }

        location /job {
            proxy_pass  http://127.0.0.1:8071/api;
        }

        location / {
            proxy_pass  http://127.0.0.1:5000;
        }

        #error_page  404              /404.html;

        # redirect server error pages to the static page /50x.html
        #
        error_page   500 502 503 504  /50x.html;
        location = /50x.html {
            root   html;
        }
    }

}
```

## 常见问题

### [emerg]: unknown directive in xxx

`nginx.conf`文件编码问题导致，更改为`UTF-8`即可
