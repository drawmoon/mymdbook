# 装机指南

> 以下以 Window10 为主。

## Chocolatey

更改执行策略，以超级管理员身份运行`PowerShell`，并执行命令。

```bash
Set-ExecutionPolicy Bypass -Scope Process
```

执行`PowerShell`命令安装。

```bash
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
```

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

## MobaXterm

利用`choco`安装。

```bash
choco install mobaxterm
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

## Yarn

利用`scoop`安装。

```bash
scoop install yarn
```

## Deno

利用`scoop`安装。

```bash
scoop install deno
```

## Hyper-V

以超级管理员身份运行`PowerShell`，并执行命令。

```bash
Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All
```

## Minikube

[安装 Minikube](https://kubernetes.io/zh/docs/tasks/tools/install-minikube/)

```bash
minikube start --iso-url='https://kubernetes.oss-cn-hangzhou.aliyuncs.com/minikube/iso/minikube-v1.13.0.iso' --image-repository=registry.cn-hangzhou.aliyuncs.com/google_containers
```

## Docker

### ~~在 Linux 子系统中安装 Docker 方案。~~

~~设置 wsl 的新分发的默认版本。~~

```bash
wsl --set-default-version 2
```

~~安装 [wsl2](https://docs.microsoft.com/zh-cn/windows/wsl/wsl2-kernel)~~

~~在 Microsoft Store 中安装 [Ubuntu 20.04 LTS](https://www.microsoft.com/zh-cn/p/ubuntu-2004-lts/9n6svws3rx71#activetab=pivot:overviewtab)。~~

~~在 Linux 子系统中安装 Docker，参考 [Ubuntu 安装指南 Docker 安装部分](./ubuntu.md#docker)。~~

~~在 wsl2 中还没有 systemctl，`sudo /etc/init.d/docker start`~~

~~查看 wsl 的分发运行状态。~~

```bash
wsl -l -v
```

~~手动切换分发的版本。~~

```bash
wsl --set-version <Name> 2
```

~~wsl 访问 windows 文件。~~

```bash
cd /mnt/c/Windows
```

~~wsl 使用 Windows 的代理（v2rayN）。~~

~~参数设置 -> v2rayN 设置 -> [x] 允许来自局域网的连接。~~

~~Ubuntu 设置代理参考 [Ubuntu 安装指南设置代理部分](./ubuntu.md#将-zsh-用作默认-shell)，代理 IP 为 Windows 的 IP。~~

## PostgreSql

安装 Docker 镜像版，参考 Docker 节点安装，在 Linux 子系统中的安装。

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
