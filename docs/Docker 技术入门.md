# Docker 技术入门

- [Docker 简介](#docker-简介)
- [安装 Docker](#安装-docker)
- [使用镜像](#使用镜像)
- [操作容器](#操作容器)
- [使用 Dockerfile 构建镜像](#使用-dockerfile-构建镜像)
- [Dockerfile 多阶段构建](#dockerfile-多阶段构建)
- [网络](#网络)
- [Docker 三剑客之 Compose](#docker-三剑客之-compose)
- [磁盘清理](#磁盘清理)

## Docker 简介

Docker 技术解决了应用的打包与交付问题，提供了只需一次打包，即可到处运行的解决方案。

**为什么要使用 Docker？**

- 更快速的交付和部署
- 更轻松的迁移和扩展
- 更高效的资源利用
- 更简单的更新管理

**Docker 对比虚拟机的优势？**

- 容器的启动和停止更快
- 容器对系统资源需求更少
- 通过镜像仓库非常方便的获取、分发、更新和存储
- 通过 Dockerfile 实现自动化部署

## 安装 Docker

### Manjaro 环境下安装 Docker

```bash
sudo pacman -S docker
```

确认 Docker 服务启动正常

```bash
sudo systemctl enable docker
sudo systemctl start docker
```

### 配置 Docker 服务

建立 Docker 用户组

```bash
sudo groupadd docker
```

将当前用户加入 Docker 组

```bash
sudo usermod -aG docker $USER
```

镜像加速

修改 `/etc/docker/daemon.json` 文件，在 `registry-mirrors` 中加入镜像市场：

```bash
{
  "registry-mirrors": [
    "http://f1361db2.m.daocloud.io"
  ]
}
```

重启 Docker 服务

```bash
sudo systemctl daemon-reload
sudo systemctl restart docker
```

执行 `info` 指令可以检查配置是否生效：

```bash
docker info
```

## 使用镜像

### 列出所有本地镜像

```bash
docker images
```

### 拉取镜像

```bash
docker pull <仓库>[:标签]
```

比如：

```bash
docker pull nginx

# 拉取指定标签的镜像
docker pull nginx:stable-alpine
```

### 删除镜像

```bash
docker rmi <仓库>
```

### 保存镜像

```bash
docker save -o myapp.tar <仓库>[:标签]

# Or
docker save <仓库>[:标签] > myapp.tar
```

> 推荐填写 `<仓库>[:标签]`，如果填写的是`镜像 ID`，`load` 进来的镜像会显示 `<none>`

使用`gzip`进行压缩

```bash
docker save <仓库>[:标签] | gzip > myapp.tar
```

### 载入镜像

```bash
docker load -i myapp.tar

# Or
docker load < myapp.tar
```

### 标记镜像

```bash
docker tag [用户/]<仓库>[:标签] [用户/]<仓库>[:标签]
```

### 将镜像上传到镜像仓库

```bash
# 登录
docker login -u <用户> -p <密码>
docker push <仓库>[:标签]
```

## 操作容器

### 列出所有容器

```bash
docker ps
```

`-a` 包含终止的容器

```bash
docker ps -a
```

### 启动

```bash
docker run <仓库>
```

`-p` 指定端口映射，映射一个端口到内部容器开放的网络端口。

```bash
docker run -p 3000:3000 <仓库>
```

`--name` 指定容器的名称

```bash
docker run --name my-web <仓库>
```

`-d` 标记容器为后台运行

```bash
docker run -d <仓库>
```

`-e` 指定环境变量

```bash
docker run -e PORT=3000 <仓库>
```

`-it` 以交互模式运行容器

```bash
docker run -it <仓库> /bin/bash

# 覆盖 Dockerfile 中的 ENTRYPOINT 启动容器
docker run -it --entrypoint /bin/bash <仓库>
```

`-v` 启动一个挂载数据卷的容器

```bash
docker run -v /home/nginx.conf:/etc/nginx/nginx.conf nginx

# Windows 下挂载数据卷语法
docker run -v //D/nginx.conf:/etc/nginx/nginx.conf nginx
```

### 启动已终止的容器

```bash
docker start <容器>
```

### 重新启动容器

```bash
docker restart <容器>
```

### 终止容器

```bash
docker stop <容器>
```

### 删除容器

```bash
docker rm <容器>
```

### 查看容器运行日志

```bash
docker logs <容器>
```

`-f` 监听容器的输出

```bash
docker logs <容器> -f
```

### 进入容器内部

```bash
docker exec -it <容器> /bin/bash
```

### 连接到正在运行的容器

```bash
docker attach <容器>
```

### 将容器保存为新的镜像

```bash
docker commit <容器> <仓库>[:标签]
```

`-m` 可以指定信息

```bash
docker commit -m "commit message" <容器> <仓库>[:标签]
```

### 拷贝容器中的文件

```bash
docker cp <容器>:/app/myapp .
```

## 使用 Dockerfile 构建镜像

创建 `Dockerfile` 文件

- `FROM`: 指定基础镜像
- `WORKDIR`: 指定工作目录
- `COPY`: 复制文件
- `ADD`: 更高级的复制命令，支持 URL
- `ENV`: 设置环境变量，示例：ENV k1=v1 k2=v2
- `RUN`: 构建镜像时运行的命令
- `EXPOSE`: 指定暴露的端口
- `USER`: 指定执行后续命令的用户和用户组
- `CMD`: 容器启动时执行的命令
- `ENTRYPOINT`: 类似 CMD，与 CMD 不同的是不会被 `docker run` 中的命令给覆盖，如果想要覆盖必须配合 `--entrypoint` 参数

```Dockerfile
FROM node
WORKDIR /app
COPY . .
RUN npm install \
  && npm run build
ENTRYPOINT ["node", "dist/main"]
```

> 如果同时设置了 `ENTRYPOINT` 和 `CMD`，当两个参数的值都是数组时，会拼接成一个命令，否则执行 `ENTRYPOINT` 中的命令

配置忽略拷贝的目录或文件，在 `Dockerfile` 相同的目录位置，创建名称为 `.dockerignore` 的文件

```conf
.git
node_modules
```

执行构建

```bash
docker build -t myapp .
```

`.` 表示 Dockerfile 所在的位置

`-f` 可以指定 Dockerfile 文件

```bash
docker build -t myapp -f Dockerfile.custom .
```

## Dockerfile 多阶段构建

为何要使用多阶段构建？

- 减少重复劳动
- 保护源代码
- 降低镜像体积

```Dockerfile
FROM node AS build
WORKDIR /source
COPY . .
RUN npm install \
    && npm run build

FROM node
WORKDIR /app
COPY --from=build /source/dist .
COPY --from=build /source/node_modules node_modules
ENTRYPOINT [ "node", "main" ]
```

## 网络

网络服务是 Docker 提供的一种容器互联的方式。

### 列出所有网络

```bash
docker network ls
```

### 创建网络

```bash
docker network create <网络>
```

`-d` 可以指定网络的类型，分别有 `bridge`、`overlay`。

```bash
docker network create -d bridge <网络>
```

### 删除网络

```bash
docker network rm <网络>
```

### 容器互联

`--network` 将容器连接到 `my-network` 网络。

```bash
docker run --network my-network --name pg-server -d postgres

docker run --network my-network --name my-web -e DB_HOST=pg-server -d web
```

## Docker 三剑客之 Compose

Compose 可以通过编写 `docker-compose.yml` 模板文件，来定义一组相关联的容器为一个项目。

### 安装 Compose

```bash
curl -L https://get.daocloud.io/docker/compose/releases/download/1.29.2/docker-compose-`uname -s`-`uname -m` > /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose
```

检查 Compose 是否成功安装

```bash
docker-compose --version
```

### Compose 模板文件

新建 `docker-compose.yml` 文件

```yml
version: "3"
services:

  db:
    image: postgres
    container_name: pg-server
    # volumes: 
    #   - /app/ioea/data/pgdata:/var/lib/postgresql-static/data
    environment:
      POSTGRES_PASSWORD: postgres
      # PGDATA: /var/lib/postgresql-static/data
    ports:
      - "5432:5432"
    restart: always

  obs:
    image: minio/minio
    container_name: minio-server
    # volumes:
    #    - /tmp/data:/tmp/data
    environment:
      MINIO_ACCESS_KEY: AKIAIOSFODNN7EXAMPLE
      MINIO_SECRET_KEY: wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
    ports:
      - "9000:9000"
    command: server /tmp/data
    restart: always

  app:
    image: app
    container_name: my-app
    environment:
      DATABASE_HOST: pg-server
      DATABASE_PORT: 5432
      DATABASE_USERNAME: postgres
      DATABASE_PASSWORD: postgres
      DATABASE_NAME: postgres
      MINIO_END_POINT: minio-server
      MINIO_PORT: 9000
      MINIO_ROOT_USER: AKIAIOSFODNN7EXAMPLE
      MINIO_ROOT_PASSWORD: wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
      MINIO_USE_SSL: "false"
    depends_on:
      - db
      - obs
    restart: always

  nginx:
    image: nginx:latest
    container_name: nginx-server
    volumes:
      - /app/ioea/conf/nginx.conf:/etc/nginx/nginx.conf
      # - /app/ioea/wwwroot:/usr/share/nginx/html
    ports:
      - "80:80"
    depends_on:
      - db
      - obs
      - app
    restart: always
```

### 启动项目

自动构建镜像、创建网络、创建并启动服务。

```bash
docker-compose up
```

`-d` 可以使项目在后台运行

```bash
docker-compose up -d
```

指定 docker-compose 配置文件

```bash
docker-compose -f docker-compose.yml up
```

指定容器

```bash
docker-compose up -d <服务>
```

### 停止项目

停止并删除网络、服务。

```bash
docker-compose down
```

### 列出项目所有的容器

```bash
docker-compose ps
```

### 查看服务的输出

```bash
docker-compose logs <服务>
```

`-f` 监听服务的输出

```bash
docker-compose logs <服务> -f
```

### 进入到服务中

```bash
docker-compose exec <服务> /bin/bash
```

### 启动服务

```bash
docker-compose start <服务>
```

### 重新启动服务

```bash
docker-compose restart <服务>
```

### 停止正在运行的服务

```bash
docker-compose stop <服务>
```

### 暂停服务

```bash
docker-compose pause <服务>
```

### 恢复服务

```bash
docker-compose unpause <服务>
```

### 删除服务

```bash
docker-compose rm <服务>
```

### 构建服务

```bash
docker-compose build <服务>
```

`--no-cache` 不使用缓存

```bash
docker-compose build --no-cache <服务>
```

## 磁盘清理

### 查看 Docker 的磁盘使用

```bash
docker system df
```

### 清理磁盘

删除关闭的容器、无用的数据卷、网络和构建缓存

```bash
docker system prune
```

### 清理构建缓存

```bash
docker builder prune
```

### 删除所有孤立的容器

```bash
docker container prune
```
