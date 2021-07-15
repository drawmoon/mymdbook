# 安装

## Docker

> Linux 发行版为 Manjaro，其他发行版的安装请参考 [文档](https://yeasy.gitbook.io/docker_practice/install)。

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

建立 Docker 用户组

```bash
sudo groupadd docker
```

将当前用户加入 Docker 组

```bash
sudo usermod -aG docker $USER
```

## 镜像加速

```bash
curl -sSL https://get.daocloud.io/daotools/set_mirror.sh | sh -s http://f1361db2.m.daocloud.io
```

> 该脚本可以将 --registry-mirror 加入到你的 Docker 配置文件 /etc/docker/daemon.json 中。适用于 Ubuntu14.04、Debian、CentOS6 、CentOS7、Fedora、Arch Linux、openSUSE Leap 42.1，其他版本可能有细微不同。更多详情请访问文档。

重启服务

```bash
sudo systemctl daemon-reload
sudo systemctl restart docker
```

检查加速是否生效

```bash
docker info
```

## Docker Compose

```bash
curl -L https://get.daocloud.io/docker/compose/releases/download/1.29.2/docker-compose-`uname -s`-`uname -m` > /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose
```
