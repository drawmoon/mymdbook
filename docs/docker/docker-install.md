# Docker 安装

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

建立 Docker 用户组

```bash
sudo groupadd docker
```

将当前用户加入 Docker 组

```bash
sudo usermod -aG docker $USER
```

## 镜像加速

修改`daemon.json`文件

```bash
vim /etc/docker/daemon.json
```

将`http://f1361db2.m.daocloud.io`添加到`registry-mirrors`数组中

```json
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
