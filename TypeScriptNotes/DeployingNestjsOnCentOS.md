# Table of contents

- CentOS 部署 NestJs 项目（Nginx + NestJs + MinIO）
  - [部署 Nginx](#部署-nginx)
  - [部署 MinIO](#部署-minio)
  - [部署 NestJs 项目](#部署-nestjs-项目)

# CentOS 部署 NestJs 项目（Nginx + NestJs + MinIO）

## 部署 Nginx

安装依赖包

- `gcc`: C 语言编译器
- `pcre-devel`: 正则表达式库
- `zlib-devel`: 数据压缩库
- `openssl`: TLS/SSL 加密库
- `make`: 解释 Makefile 的命令工具

```bash
sudo yum install gcc pcre-devel zlib-devel openssl openssl-devel make
```

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
# 编译
make

# 安装
make install
```

启动 Nginx

```bash
cd /usr/local/nginx/sbin/
./nginx
```

验证 Nginx 是否成功启动

```bash
# 查看 Nginx 进程
ps -ef | grep nginx # 如果显示了 Nginx 的进程，说明已经成功启动

# 请求 80 端口
curl http://127.0.0.1 # 如果看到了 Welcome to nginx!，说明已经成功启动
```

将 Nginx 添加到全局变量中

```bash
In -s /usr/local/nginx/sbin/nginx /usr/local/bin/

# 测试
nginx -v
```

## 部署 MinIO

```bash
export MINIO_ROOT_USER=AKIAIOSFODNN7EXAMPLE
export MINIO_ROOT_PASSWORD=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
./minio server ./data

# 后台执行
nohup ./minio server ./data &
```

Nginx 配置 MinIO Browser 代理

```conf
# nginx.conf
server {
  listen      80;
  server_name localhost;

  location /minio {
    proxy_pass  http://127.0.0.1:9000;
  }

  location / {
    root  html;
    index index.html index.htm;
  }
}
```

重新加载 Nginx 配置

```bash
nginx -s reload
```

## 部署 NestJs 项目

安装依赖包

```bash
# 添加 Nodesource 包存储库
curl --silent --location https://rpm.nodesource.com/setup_10.x | sudo bash -

# 安装 Node
sudo yum install nodejs
```

打包 NestJs 项目

```bash
npm run build
```

打包成功后，将`package.json`拷贝到`dist`文件夹中，一起上传到服务器中

安装项目依赖包，并启动服务

```bash
npm install
node main

# 后台执行
nohup node main &
```

配置 Nginx 代理

```conf
server {
  listen      1082;
  server_name localhost;

  location /api {
    proxy_pass  http://127.0.0.1:3000/api;
  }

  location / {
    root    html;
    index   index.html index.htm;
  }
}
```
