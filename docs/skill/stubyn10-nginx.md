# 学习笔记10: Nginx

- [核心配置指令](#核心配置指令)

## 核心配置指令

### 代理

```conf
server {
    listen       80;
    server_name  localhost;

    location / {
        proxy_pass http://127.0.0.1:5000;
    }
}
```

### 搭建静态资源服务器

```conf
server {
    listen       80;
    server_name  localhost;

    location / {
        root   /usr/share/nginx/html;
        index  index.html index.htm;
    }
}
```

### 虚拟路径配置

```conf
server {
    listen       80;
    server_name  localhost;

    location /document {
        alias  /usr/share/nginx/html/doc;
        autoindex on;
    }
}
```

### 添加请求头字段

`proxy_set_header` 可将指定字段添加到请求头。

```conf
server {
    listen       80;
    server_name  localhost;

    location / {
        proxy_set_header X-Test TestValue;
        proxy_pass http://127.0.0.1:5000;
    }
}
```

### 添加响应头字段

默认情况下，当响应状态码为 `200, 201, 204, 206, 301, 302, 303, 304, 307, 308` 时， `add_header` 可将指定字段添加到响应头。

```conf
server {
    listen       80;
    server_name  localhost;

    location / {
        add_header X-Test TestValue;
        proxy_pass http://127.0.0.1:5000;
    }
}
```

当指定为 `always` 时，则始终都会将指定字段添加到响应头。

```conf
server {
    listen       80;
    server_name  localhost;

    location / {
        add_header X-Test TestValue always;
        proxy_pass http://127.0.0.1:5000;
    }
}
```
