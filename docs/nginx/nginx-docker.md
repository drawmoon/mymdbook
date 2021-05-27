# Nginx 镜像使用笔记

- [挂载外部的配置文件](#挂载外部的配置文件)
- [为多个容器配置反向代理](#为多个容器配置反向代理)

## 挂载外部的配置文件

```bash
docker run --name nginx -v /home/conf/nginx/nginx.conf:/etc/nginx/nginx.conf:ro -p 80:80 -d nginx
```

## 为多个容器配置反向代理

默认情况下容器与容器、容器与宿主机的网络是隔离的，若想在多个容器间互相通信，必须将容器放在同一个网络上

```bash
docker network create myappnet
docker run --net myappnet --name file_service -p 3000:3000 -d file-service
docker run --net myappnet --name job_service -p 8071:8071 -d job-service
docker run --name nginx -v ./nginx.conf:/etc/nginx/nginx.conf:ro -p 80:80 -d nginx
```

`nginx.conf`配置

```conf
server {
    listen       80;
    server_name  localhost;

    location /api/fs {
        proxy_pass  http://file_service:3000/api; # 在同一个网络上，只需要填写容器名称，Docker 会自动 DNS 解析容器名称到 IP 地址
    }

    location /job {
        proxy_pass  http://job_service:8071/api;
    }

    location / {
        proxy_pass  http://file_service:3000;
    }
}
```
