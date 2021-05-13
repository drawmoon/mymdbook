# Nestjs app 集成部署笔记

- [在 Linux 环境中部署]
- [在 Docker 环境中部署]
- [利用 Docker Compose 部署]
- [Gitlab 集成部署]

## 在 Linux 环境中部署

### 安装 Nginx

安装依赖项

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

### 安装 MinIO

```bash
wget http://dl.minio.org.cn/server/minio/release/linux-amd64/minio
export MINIO_ROOT_USER=AKIAIOSFODNN7EXAMPLE
export MINIO_ROOT_PASSWORD=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
./minio server ./data
```

### 安装 Chrome

```bash
wget https://dl.google.com/linux/direct/google-chrome-stable_current_x86_64.rpm
yum install google-chrome-stable_current_x86_64.rpm
```

### 部署应用

构建 Nestjs 项目

```bash
npm run build
```

打包成功后，将`package.json`、`package-lock.json`和`.env`文件拷贝到`dist`文件夹中，一起压缩后上传到服务器中

```bash
# 安装项目依赖包
npm install

# 后台启动服务
nohup node main &
```

### 配置 Nginx 代理

```conf
vi /usr/local/nginx/conf/nginx.conf

# nginx.conf
server {
  listen      80;
  server_name localhost;

  location /api/fs {
    proxy_pass  http://127.0.0.1:3000/api;
  }

  location /job {
    proxy_pass  http://127.0.0.1:8071/api;
  }

  location / {
    proxy_pass  http://127.0.0.1:5000;
  }
}
```

重新加载 Nginx 配置

```bash
nginx -s reload
```

## 在 Docker 环境中部署

在根目录创建`Dockerfile`

```Dockerfile
FROM node
WORKDIR /app
RUN apt-get update \
  && apt-get install -y wget gnupg ca-certificates \
  && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
  && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' \
  && apt-get update \
  # We install Chrome to get all the OS level dependencies, but Chrome itself
  # is not actually used as it's packaged in the node puppeteer library.
  # Alternatively, we could could include the entire dep list ourselves
  # (https://github.com/puppeteer/puppeteer/blob/master/docs/troubleshooting.md#chrome-headless-doesnt-launch-on-unix)
  # but that seems too easy to get out of date.
  && apt-get install -y google-chrome-stable \
  && rm -rf /var/lib/apt/lists/*
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
docker run --name app1 -p 3000:3000 -d docker.k8s/app:latest
```
