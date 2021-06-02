# Docker 常用命令

## 拉取镜像

```bash
docker pull ubuntu:latest
```

## 运行容器

```bash
# 将本地 3333 端口映射为容器 3000 端口
docker run -p 3333:3000 --name myapp myapp:latest

# 后台运行
docker run -p 3333:3000 --name myapp -d myapp:latest

# 设置环境变量
docker run -p 3333:3000 -e AUTHORITY=http://localhost:5000 --name myapp myapp:latest

# 以交互模式运行容器
docker run -it ubuntu:latest /bin/bash

# 覆盖 Dockerfile 中的 ENTRYPOINT 运行容器
docker run -it --entrypoint /bin/bash myapp

# 文件映射
docker run --name myapp -v /home/me/desc.txt:/app myapp:latest
# Windows 文件映射
docker run --name myapp -v //D/desc.txt:/app myapp:latest
```

## 查看容器运行日志

```bash
docker logs 1b

# 动态日志
docker logs 1b -f

# 1b 表示容器的 ID
```

## 启动/停止容器

```bash
docker start/stop 1b

# 1b 表示容器的 ID
```

## 进入容器内部

```bash
docker exec -it 1b /bin/bash

# 1b 表示容器的 ID
```

## 连接到正在运行的容器

```bash
docker attach 1b

# 1b 表示容器的 ID
```

## 构建镜像

创建`Dockerfile`文件

```Dockerfile
FROM ubuntu:latest                # 指定基础镜像
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
docker save -o myapp.tar myapp:latest # 这里推荐填写镜像名称:标记号，如果填写的是镜像 ID，load 进来的镜像会显示 <none>

# or
docker save myapp:latest > myapp.tar

# zip
docker save myapp:latest | gzip > myapp-image
```

## 载入镜像

```bash
docker load -i myapp.tar

# or
docker load < myapp.tar

# -i 完整的指令为 --input
```

## 将容器保存为新的镜像

```bash
docker commit 1b drsh/myapp:1.0

# 语法：
# docker commit <container-id> <user>/<image-name>:<tag>
```

## 标记镜像

```bash
docker tag myapp:1.0 myapp:2.0

# 语法：
# docker tag <image-name>:<tag> [username/]<image-name>:<tag>
```

## 将镜像上传到镜像仓库

```bash
# 登录
docker login -u drsh -p 123456
docker push drsh/myapp:1.0
```

## 拷贝容器中的文件

```bash
docker cp 1b:/app/myapp .

# 1b 表示容器的 ID
```

## 删除容器/镜像

```bash
docker rm/rmi 1b

# 1b 表示容器/镜像的 ID
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
