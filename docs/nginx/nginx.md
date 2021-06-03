# Nginx 使用笔记

- [HTTP 代理](#http-代理)
- [搭建静态资源服务器](#搭建静态资源服务器)
- [虚拟路径配置](#虚拟路径配置)

## HTTP 代理

```conf
server {
    listen       80;
    server_name  localhost;

    location / {
        proxy_pass http://127.0.0.1:5000;
    }
}
```

## 搭建静态资源服务器

```conf
server {
    listen       80;
    server_name  localhost;

    location /api {
        proxy_pass http://127.0.0.1:5000/api;
    }

    location / {
        root   /usr/share/nginx/html;
        index  index.html index.htm;
    }
}
```

## 虚拟路径配置

```conf
server {
    listen       80;
    server_name  localhost;

    location /api {
        proxy_pass http://127.0.0.1:5000/api;
    }

    location /document {
        alias  /usr/share/nginx/html/document;
        autoindex on;
    }

    location / {
        root   /usr/share/nginx/html;
        index  index.html index.htm;
    }
}
```
