# 部署 NestJs 应用

- [安装 Nginx](#安装-nginx)
- [部署 MinIO](#部署-minio)
- [部署 NestJs 项目，或通过 Docker 部署](#部署-nestjs-项目，或通过-docker-部署)

## 安装 Nginx

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
ln -s /usr/local/nginx/sbin/nginx /usr/local/bin/

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
vi /usr/local/nginx/conf/nginx.conf

# nginx.conf
server {
  listen      80;
  server_name localhost;

  location /minio {
    proxy_pass  http://127.0.0.1:9000;
  }
}
```

重新加载 Nginx 配置

```bash
nginx -s reload
```

## 部署 NestJs 项目，或通过 Docker 部署

### 部署在服务器上

安装依赖包

```bash
# 添加 Nodesource 包存储库
curl --silent --location https://rpm.nodesource.com/setup_10.x | sudo bash -

# 安装 Node
sudo yum install nodejs
```

构建 NestJs 项目

```bash
npm run build
```

打包成功后，将`package.json`、`package-lock.json`和`.env`文件拷贝到`dist`文件夹中，一起压缩后上传到服务器中

```bash
# 安装项目依赖包
npm install

# 启动服务
nohup node main &
```

### 通过 Docker 部署

在根目录创建`Dockerfile`

```Dockerfile
FROM node
WORKDIR /app
COPY package*.json ./
RUN npm --version \
    && npm install
COPY dist ./
COPY .env ./
ENTRYPOINT ['node', 'main']
```

构建并推送 Docker 镜像

```bash
docker build -t app:latest .
docker login docker.k8s -u admin -p admin
docker push app:latest
```

在服务器中拉取并运行镜像

```bash
docker pull docker.k8s/app:latest
docker run -p 3000:3000 -d docker.k8s/app:latest
```

### 配置 Nginx 代理

```conf
vi /usr/local/nginx/conf/nginx.conf

# nginx.conf
server {
  listen      80;
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

重新加载 Nginx 配置

```bash
nginx -s reload
```
