# Docker 安装

## Ubuntu

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

## Manjaro

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

## 镜像加速

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
