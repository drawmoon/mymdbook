# Windows 10 Notes

- [Windows 终端](#windows-终端)
- [截图、贴图](#截图贴图)
- [Dbeaver](#dbeaver)
- [Web 调试代理工具](#web-调试代理工具)
- [安装 WSL](#安装-wsl)
- [在 WSL 中安装 Docker](#在-wsl-中安装-docker)
- [安装 Docker 镜像版的 PostgreSql](#安装-docker-镜像版的-postgresql)
- [安装 Minikube](#安装-minikube)
- [安装 Yarn](#安装-yarn)

## Windows 终端

- [Windows Terminal](https://www.microsoft.com/zh-cn/p/windows-terminal/9n0dx20hk701?activetab=pivot:overviewtab)
- [MobaXterm](https://mobaxterm.mobatek.net/)

## 截图、贴图

[官网](https://zh.snipaste.com/)

## Dbeaver

[阿里云云效 Maven](https://maven.aliyun.com/mvn/guide)

窗口 > 首选项 > 连接 > 驱动 > Maven

```txt
ID: aliyun-maven-repo
Name: 阿里云仓库
URL: https://maven.aliyun.com/repository/public/
```

## Web 调试代理工具

```bash
npm install -g whistle
```

Example

```conf
http://report.net/api/1.0/file http://localhost:3000/api/1.0/file

http://report.net http://localhost:5000
```

[whistle 教程](https://wproxy.org/whistle/)

## 安装 WSL

安装适用于 Linux 的 Windows 的子系统，安装 WSL2 之前，必须启用`虚拟机平台`

```bash
dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart
```

下载并安装 [WSL2](https://docs.microsoft.com/zh-cn/windows/wsl/wsl2-kernel)

设置 WSL2 为默认版本

```bash
wsl --set-default-version 2
```

在 Microsoft Store 中安装 [Ubuntu 20.04 LTS](https://www.microsoft.com/zh-cn/p/ubuntu-2004-lts/9n6svws3rx71#activetab=pivot:overviewtab)

查看 WSL 的分发运行状态

```bash
wsl -l -v
```

手动切换分发的版本

```bash
wsl --set-version <Name> 2
```

WSL 访问 Windows 文件

```bash
cd /mnt/c/Windows
```

WSL 使用 Windows 的代理

如果代理客户端使用的是 v2rayN，首先需要配置 v2rayN ☑ 允许来自局域网的连接。

```bash
export hostip=$(cat /etc/resolv.conf |grep -oP '(?<=nameserver\ ).*')
export https_proxy="http://${hostip}:10809"
export http_proxy="http://${hostip}:10809"
```

## 在 WSL 中安装 Docker

安装 Docker

```bash
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh get-docker.sh
```

启用 Docker，由于 WSL2 不支持 `systemctl`，所以用`service`替代

```bash
sudo service docker start
```

检查 Docker 进程是否启动

```bash
service docker status
```

建立 docker 用户组

```bash
sudo groupadd docker
sudo usermod -aG docker $USER
```

镜像加速

```bash
sudo vim /etc/docker/daemon.json

{
  "registry-mirrors": [
    "http://f1361db2.m.daocloud.io"
  ]
}
```

## 安装 Docker 镜像版的 PostgreSql

拉取 PostgreSql 镜像

```bash
docker pull postgres:latest
```

启动 Postgres

```bash
docker run --name postgres -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres:latest
docker container start postgres
```

## 安装 Minikube

启用 Hyper-V

以超级管理员身份运行`PowerShell`，并执行命令

```bash
Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All
```

[安装 Minikube](https://kubernetes.io/zh/docs/tasks/tools/install-minikube/)

```bash
minikube start --iso-url='https://kubernetes.oss-cn-hangzhou.aliyuncs.com/minikube/iso/minikube-v1.13.0.iso' --image-repository=registry.cn-hangzhou.aliyuncs.com/google_containers
```

## 安装 Yarn

```bash
npm install -g yarn
```

更改 PowerShell 执行策略

```bash
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```
