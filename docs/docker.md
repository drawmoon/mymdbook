# Docker Notes

- [安装 Docker](#安装-docker)
- [Docker 命令](#docker-命令)

## 安装 Docker

### Ubuntu

安装依赖包

```bash
sudo apt-get install \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg \
    lsb-release
```

添加软件源

```bash
# 添加软件源 gpg 密钥
curl -fsSL https://mirrors.aliyun.com/docker-ce/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

echo \
  "deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://mirrors.aliyun.com/docker-ce/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

安装 Docker

```bash
sudo apt-get update
sudo apt-get install docker-ce
```

启用 Docker

```bash
sudo systemctl enable docker
sudo systemctl start docker

# 查看 Docker 状态
sudo systemctl status docker
```

建立 docker 用户组

```bash
# 建立 Docker 用户组
sudo groupadd docker
# 将当前用户加入 Docker 组
sudo usermod -aG docker $USER
```

### Manjaro

```bash
sudo pacman -S docker
```

启用 Docker

```bash
sudo systemctl enable docker
sudo systemctl start docker

# 查看 Docker 状态
sudo systemctl status docker
```

建立 docker 用户组

```bash
# 建立 Docker 用户组
sudo groupadd docker
# 将当前用户加入 Docker 组
sudo usermod -aG docker $USER
```

### 镜像加速

```bash
vim /etc/docker/daemon.json

{
  "registry-mirrors": [
    "http://f1361db2.m.daocloud.io"
  ]
}
```

重启服务

```bash
sudo systemctl daemon-reload
sudo systemctl restart docker
```

检查加速是否生效

```bash
docker info
```

## Docker 命令

### 拉取镜像

```bash
docker pull ubuntu:latest
```

### 运行容器

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
```

### 启动容器

```bash
docker start 1b

# 1b 表示容器的 ID
```

### 停止容器

```bash
docker stop 1b

# 1b 表示容器的 ID
```

### 连接到正在运行的容器

```bash
docker attach 1b

# 1b 表示容器的 ID
```

### 进入容器内部

```bash
docker exec -it 1b /bin/bash

# 1b 表示容器的 ID
```

### 构建镜像

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

### 保存镜像

```bash
docker save -o myapp.tar myapp:latest

# or
docker save myapp:latest > myapp.tar

# zip
docker save myapp:latest | gzip > myapp-image
```

### 载入镜像

```bash
docker load -i myapp.tar

# or
docker load < myapp.tar

# -i 完整的指令为 --input
```

### 将容器保存为新的镜像

```bash
docker commit 1b drsh/myapp:1.0

# 语法：
# docker commit <container-id> <user>/<image-name>:<tag>
```

### 标记镜像

```bash
docker tag myapp:1.0 myapp:2.0

# 语法：
# docker tag <image-name>:<tag> [username/]<image-name>:<tag>
```

### 将镜像上传到镜像仓库

```bash
# 登录
docker login -u drsh -p 123456
docker push drsh/myapp:1.0
```

### 删除容器

```bash
docker rm 1b

# 1b 表示容器的 ID
```

### 删除镜像

```bash
docker rmi cc

# cc 表示镜像的 ID
```

### 示例

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

### 查看 Docker 的磁盘使用

```bash
docker system df
```

### 清理磁盘

删除关闭的容器、无用的数据卷、网络和构建缓存

```bash
docker system prune
```
