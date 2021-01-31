# Windows 10 Notes

## Scoop

更改执行策略，以超级管理员身份运行`PowerShell`，并执行命令。

```bash
Set-ExecutionPolicy RemoteSigned -scope CurrentUser
```

执行`PowerShell`命令安装。

```bash
Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')
```

## Git

利用`scoop`安装。

```bash
scoop install git
```

## Nodejs

利用`scoop`安装。

```bash
scoop install nodejs
```

## TypeScript

利用`npm`安装。

```bash
npm install -g typescript
```

## Docker

安装适用于 Linux 的 Windows 的子系统

安装 WSL2 之前，必须启用`虚拟机平台`

```bash
dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart
```

下载并安装 [WSL2](https://docs.microsoft.com/zh-cn/windows/wsl/wsl2-kernel)

设置 WSL2 为默认版本

```bash
wsl --set-default-version 2
```

在 Microsoft Store 中安装 [Ubuntu 20.04 LTS](https://www.microsoft.com/zh-cn/p/ubuntu-2004-lts/9n6svws3rx71#activetab=pivot:overviewtab)

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
    "https://hub-mirror.c.163.com",
    "https://mirror.baidubce.com"
  ]
}
```

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

配置 v2rayN，参数设置 -> v2rayN 设置 -> [x] 允许来自局域网的连接。

~~Ubuntu 设置代理参考 [Ubuntu 安装指南设置代理部分](./ubuntu.md#将-zsh-用作默认-shell)，代理 IP 为 Windows 的 IP。~~

## PostgreSql

在 Docker 中运行 PostgreSql。

```bash
docker pull postgres:latest
docker run --name postgres -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres:latest
```

启动 postgres

```bash
docker container start postgres
```

## whistle

利用`npm`安装。

```bash
npm install -g whistle
```

[whistle 教程](https://wproxy.org/whistle/)

## Snipaste

[官网](https://zh.snipaste.com/)

## MobaXterm

[官网](https://mobaxterm.mobatek.net/)

## Yarn

利用`scoop`安装。

```bash
scoop install yarn
```

## Hyper-V

以超级管理员身份运行`PowerShell`，并执行命令

```bash
Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All
```

## Minikube

[安装 Minikube](https://kubernetes.io/zh/docs/tasks/tools/install-minikube/)

```bash
minikube start --iso-url='https://kubernetes.oss-cn-hangzhou.aliyuncs.com/minikube/iso/minikube-v1.13.0.iso' --image-repository=registry.cn-hangzhou.aliyuncs.com/google_containers
```

## Deno

利用`scoop`安装。

```bash
scoop install deno
```
