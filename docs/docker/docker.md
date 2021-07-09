# Docker 常用命令

## 拉取镜像

```bash
docker pull <仓库>
```

拉取指定标签的镜像

```bash
docker pull <仓库>[:标签]
```

比如：

```bash
docker pull nginx:stable-alpine
```

## 运行容器

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

# 覆盖 Dockerfile 中的 ENTRYPOINT 运行容器
docker run -it --entrypoint /bin/bash <仓库>
```

`-v` 启动一个挂载数据卷的容器

```bash
docker run -v /home/nginx.conf:/etc/nginx/nginx.conf nginx

# Windows 下挂载数据卷语法
docker run -v //D/nginx.conf:/etc/nginx/nginx.conf nginx
```

## 查看容器运行日志

```bash
docker logs <容器>
```

`-f` 监听容器的输出

```bash
docker logs <容器> -f
```

## 启动/停止/重新启动容器

```bash
docker start/stop/restart <容器>
```

## 进入容器内部

```bash
docker exec -it <容器> /bin/bash
```

## 连接到正在运行的容器

```bash
docker attach <容器>
```

## 构建镜像

创建`Dockerfile`文件

```Dockerfile
FROM node                         # 指定基础镜像
WORKDIR /app                      # 指定工作目录
COPY . .                          # 复制文件
RUN npm --version \               # 构建镜像时运行的命令
  && npm install \
  && npm run build
ENTRYPOINT ["node", "dist/main"]  # 类似 CMD，与 CMD 不同的是不会被 docker run 中的命令给覆盖，如果想要覆盖必须配合 --entrypoint 参数

# USER                            # 指定执行后续命令的用户和用户组
# ADD                             # 更高级的复制命令，支持 URL
# CMD                             # 容器启动时执行的命令
# EXPOSE                          # 指定暴露的端口
# ENV                             # 设置环境变量，示例：ENV k1=v1 k2=v2
```

> 如果同时设置了`ENTRYPOINT`和`CMD`，当两个参数的值都是数组时，会拼接成一个命令，否则执行`ENTRYPOINT`中的命令

如果想在拷贝文件时忽略某些目录或文件，在`Dockerfile`相同的目录位置，创建名称为`.dockerignore`的文件

```conf
.git
node_modules
```

执行 Docker 构建命令

```bash
docker build -t myapp .

# . 表示 Dockerfile 所在的位置
```

## 保存镜像

```bash
docker save -o myapp.tar [用户/]<仓库>[:标签] # 这里推荐填写仓库:标签，如果填写的是镜像 ID，load 进来的镜像会显示 <none>

# Or
docker save [用户/]<仓库>[:标签] > myapp.tar
```

使用`gzip`进行压缩

```bash
docker save [用户/]<仓库>[:标签] | gzip > myapp.tar
```

## 载入镜像

```bash
docker load -i myapp.tar

# Or
docker load < myapp.tar
```

## 将容器保存为新的镜像

```bash
docker commit <容器> [用户/]<仓库>[:标签]
```

## 标记镜像

```bash
docker tag [用户/]<仓库>[:标签] [用户/]<仓库>[:标签]
```

## 将镜像上传到镜像仓库

```bash
# 登录
docker login -u <用户> -p <密码>
docker push <用户/><仓库>[:标签]
```

## 拷贝容器中的文件

```bash
docker cp <容器>:/app/myapp .
```

## 删除容器/镜像

```bash
docker rm/rmi <容器/仓库>
```

## 示例

执行 Docker 命令，拉取`ubuntu`镜像，修改镜像并推送到个人仓库中

```bash
docker pull ubuntu:latest
docker run -it ubuntu:latest /bin/bash # 容器 Id 为 95551e110282
apt-get install zsh
exit
docker commit -m "install zsh" 9555 drsh/ubuntu:zsh
docker login -u drsh -p 123456
docker push drsh/ubuntu:zsh
```
