# Docker Notes

## 拉取镜像

```bash
docker pull ubuntu:latest
```

## 运行容器

### 运行容器，将容器3000端口映射到本地3333端口

```bash
docker run -p 3333:3000 --name myubuntu ubuntu:latest

# 后台运行
docker run -p 3333:3000 --name myubuntu -d ubuntu:latest

# Ctrl+p Ctrl+q 退出，并且不停止容器运行
```

### 以交互模式运行容器

```bash
docker run -it ubuntu:latest /bin/bash

# 覆盖 Dockerfile 中的 ENTRYPOINT 运行容器
docker run -it --entrypoint /bin/bash myapp
```

### 连接到正在运行的容器

```bash
docker attach 1b

# 1b 表示容器的 ID
```

### 启动容器

```bash
docker start 1b
```

### 停止容器

```bash
docker stop 1b

# 1b 表示容器的 ID
```

## 构建镜像

### Dockerfile

```Dockerfile
FROM ubuntu:latest                  # 
WORKDIR /app                        #
COPY . .                            #
RUN npm run build                   #
ENTRYPOINT ["node", "dist/main"]    #

# USER root                         #
# ADD                               #
# CMD                               #
```

```bash
docker build -t myapp .

# . 表示 Dockerfile 所在的位置
```

## 保存镜像

```bash
docker save -o myapp.tar myapp:latest
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
docker push myapp:1.0
```

## 删除容器

```bash
docker rm 1b

# 1b 表示容器的 ID
```

## 删除镜像

```bash
docker rmi myapp
```
