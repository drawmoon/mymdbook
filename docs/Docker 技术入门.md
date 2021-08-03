# Docker 技术入门

- [Docker 简介](#docker-简介)
- [安装 Docker](#安装-docker)
- [使用镜像](#使用镜像)
- [操作容器](#操作容器)
- [使用 Dockerfile 构建镜像](#使用-dockerfile-构建镜像)
- [Dockerfile 多阶段构建](#dockerfile-多阶段构建)
- [构建 ASP.NET Core 应用镜像](#构建-aspnet-core-应用镜像)
- [构建 Tomcat Web 应用镜像](#构建-tomcat-web-应用镜像)
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

### 查看镜像

#### 列出本地镜像：

```bash
docker images
```

`-a` 列出所有镜像\
`-f` 过滤镜像，例如：`-f name=nginx`\
`-q` 仅输出 ID 信息

> 更多命令可以通过 `man docker-images` 查看

#### 查看详细信息

```bash
docker inspect nginx
```

#### 查看历史记录

```bash
docker history nginx
```

### 搜索镜像

```bash
docker search nginx
```

`-f` 过滤镜像，例如：`-f stars=10`\
`--limit` 限制输出结果个数

### 拉取镜像

```bash
docker pull nginx
```

拉取指定标签的镜像：

```bash
docker pull nginx:stable-alpine
```

### 删除和清理镜像

#### 删除镜像

```bash
docker rmi nginx
```

`-f` 强制删除镜像

#### 清理镜像

`prune` 可以清理临时镜像和没有被使用过的镜像

```bash
docker image prune
```

`-a` 删除所有无用的镜像\
`-filter` 过滤镜像\
`-f` 强制删除镜像

### 保存和载入镜像

#### 保存镜像

```bash
docker save -o nginx.tar nginx
```

使用 `>` 保存镜像

```bash
docker save nginx > nginx.tar
```

使用 `gzip` 进行压缩

```bash
docker save nginx | gzip > nginx.tar
```

#### 载入镜像

```bash
docker load -i nginx.tar
```

使用 `<` 载入镜像

```bash
docker load < nginx.tar
```

### 导入镜像

```bash
docker import nginx.tar nginx
```

### 标记镜像

```bash
docker tag nginx nginx:custom
```

### 将镜像上传到镜像仓库

首先通过 `login` 登录到注册服务器，再通过 `push` 上传镜像到仓库

```bash
docker tag nginx user/nginx
docker login -u user -p 123456
docker push user/nginx
```

## 操作容器

### 查看容器

#### 列出所有容器

```bash
docker ps
```

`-a` 包含终止的容器\
`-q` 仅输出 ID 信息 

#### 查看容器详情

```bash
docker container inspect some-nginx
```

#### 查看容器内进程

指令效果与 Linux 下 `top` 命令类似，包括 PID 等信息

```bash
docker top some-nginx
```

#### 查看统计信息

通过 `stats` 指令可查看容器的 CPU、内存、存储、网络等情况的统计信息

```bash
docker stats some-nginx
```

### 创建容器

#### 新建容器

```bash
docker create nginx
```

`-p` 指定端口映射，映射一个端口到内部容器开放的网络端口，例如：`-p 3000:3000`\
`--name` 指定容器的名称，例如：`--name some-nginx`\
`-d` 标记容器为后台运行\
`-e` 指定环境变量，例如：`-e ENV=xxx`\
`-it` 以交互模式运行容器，例如：`-it nginx bash`
`--entrypoint` 镜像指定了 `ENTRYPOINT` 时，覆盖入口命令，例如：`-it --entrypoint bash nginx`\
`-v` 挂载主机上的文件卷到容器内，例如：`-v /conf/nginx.conf:/etc/nginx/nginx.conf`

> Windows 下挂载文件卷：`-v //D/nginx.conf:/etc/nginx/nginx.conf`

#### 启动容器

```bash
docker start some-nginx
```

#### 新建并启动容器

```bash
docker run nginx
```

#### 重新启动容器

```bash
docker restart some-nginx
```

### 停止容器

#### 暂停容器

```bash
docker pause some-nginx
```

#### 终止容器

```bash
docker stop some-nginx
```

### 删除和清理容器

#### 删除容器

```bash
docker rm some-nginx
```

`-f` 强制删除\
`-l` 删除容器的连接，但不会删除容器\
`-v` 删除容器挂载的数据卷

#### 清理容器

```bash
docker container prune
```

### 查看容器运行日志

```bash
docker logs some-nginx
```

`-f` 监听容器的输出

### 进入容器

#### attach 指令

```bash
docker attach some-nginx
```

`--detach-keys` 指定退出 `attach` 的快捷键，默认是 `Ctrl p Ctrl q`

#### exec 指令

`exec` 是比 `attach` 更方便的指令，可以在不影响容器内的应用情况下，打开一个新的交互界面

```bash
docker exec -it some-nginx bash
```

`-d` 在容器中后台执行命令\
`-e` 指定环境变量\
`-it` 以交互模式进入容器\
`-u` 设置执行命令的用户\
`--privileged` 分配最高权限，例如：`--privileged=true`\
`--detach-keys` 指定退出 `exec` 的快捷键

### 导出容器

导出后的本地文件，再次通过 `import` 导入为新的镜像，相比 `save`，`export` 指令只会导出当时容器的状态，不包含元数据和历史记录信息

```bash
docker export -o nginx.tar some-nginx
```

使用 `>` 导出容器

```bash
docker export some-nginx > nginx.tar
```

### 将容器保存为新的镜像

```bash
docker commit some-nginx 
```

`-m` 可以指定信息，例如：`-m "Commit Message"`

### 拷贝容器中的文件

```bash
docker cp some-nginx:/app/myapp .
```

## 使用 Dockerfile 构建镜像

创建 `Dockerfile` 文件

- `FROM`: 指定基础镜像
- `WORKDIR`: 指定工作目录
- `COPY`: 复制文件
- `ADD`: 更高级的复制命令，支持 URL
- `ARG`: 定义构建镜像时使用的变量
- `ENV`: 设置环境变量，示例：`ENV k1=v1 k2=v2`
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

## 构建 ASP.NET Core 应用镜像

`dotnet/sdk` 镜像用于生成应用\
`dotnet/aspnet` 镜像包含 ASP.NET Core 运行时，用于运行应用

```Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
COPY WebApplication.sln .
COPY WebApplication WebApplication
RUN dotnet restore \
  && dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS prod
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "WebApplication.dll" ]
```

## 构建 Tomcat Web 应用镜像

```Dockerfile
FROM openjdk:8-slim AS build
WORKDIR /source
COPY source .
RUN chmod +x ./mvnw \
    && ./mvnw clean install package

FROM tomcat:8-jdk8
COPY run.sh .
COPY --from=build /source/target/example-0.0.1-SNAPSHOT.war webapps/
ENV TZ=PRC
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone \
	&& chmod +x ./run.sh
ENTRYPOINT ["./run.sh"]
```

编写 `run.sh` 脚本文件， 内容如下：

```bash
#!/bin/bash

cd /usr/local/tomcat/bin
./startup.sh
cd /usr/local/tomcat/logs
if [ ! -f catalina.out ]
then
   sleep 3s
fi
tail -f catalina.out
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
