# Ubuntu Notes

- [修改源](#修改源)
- [将 zsh 设置为默认的 Shell](#将-zsh-设置为默认的-shell)
- [安装 OpenSSH](#安装-openssh)
- [安装 Docker](安装-docker)
- [安装 PostgreSql](#安装-postgresql)
- [安装 .NET SDK](#安装-net-sdk)
- [安装 NodeJs](#安装-nodejs)
- [安装 TypeScript](#安装-typescript)
- [安装 Whistle](#安装-whistle)
- [安装 Yarn](#安装-yarn)
- [安装 Nginx](#安装-nginx)

## 修改源

```bash
# 安装 vim
sudo apt install vim
# 或用 vi 进行编辑 sudo vi /etc/apt/source.list

# 修改文件
sudo vim /etc/apt/source.list

# 修改 http://archive.ubuntu.com/ubuntu 为下面服务器列表中的任意一个服务商地址
# source.list

# deb-src http://archive.ubuntu.com/ubuntu/ focal-updates main restricted

## N.B. software from this repository is ENTIRELY UNSUPPORTED by the Ubuntu
## team. Also, please note that software in universe WILL NOT receive any
## review or updates from the Ubuntu security team.
deb http://mirrors.163.com/ubuntu/ focal universe
# deb-src http://archive.ubuntu.com/ubuntu/ focal universe
deb http://mirrors.163.com/ubuntu/ focal-updates universe
# deb-src http://archive.ubuntu.com/ubuntu/ focal-updates universe

## N.B. software from this repository is ENTIRELY UNSUPPORTED by the Ubuntu
## team, and may not be under a free licence. Please satisfy yourself as to
## your rights to use the software. Also, please note that software in
## multiverse WILL NOT receive any review or updates from the Ubuntu
## security team.
deb http://mirrors.163.com/ubuntu/ focal multiverse
# deb-src http://archive.ubuntu.com/ubuntu/ focal multiverse
deb http://mirrors.163.com/ubuntu/ focal-updates multiverse
# deb-src http://archive.ubuntu.com/ubuntu/ focal-updates multiverse

## N.B. software from this repository may not have been tested as
## extensively as that contained in the main release, although it includes
## newer versions of some applications which may provide useful features.
## Also, please note that software in backports WILL NOT receive any review
## or updates from the Ubuntu security team.
deb http://mirrors.163.com/ubuntu/ focal-backports main restricted universe multiverse
# deb-src http://archive.ubuntu.com/ubuntu/ focal-backports main restricted universe multiverse

## Uncomment the following two lines to add software from Canonical's
## 'partner' repository.
## This software is not part of Ubuntu, but is offered by Canonical and the
## respective vendors as a service to Ubuntu users.
# deb http://archive.canonical.com/ubuntu focal partner
# deb-src http://archive.canonical.com/ubuntu focal partner

deb http://security.ubuntu.com/ubuntu/ focal-security main restricted
# deb-src http://security.ubuntu.com/ubuntu/ focal-security main restricted
deb http://security.ubuntu.com/ubuntu/ focal-security universe
# deb-src http://security.ubuntu.com/ubuntu/ focal-security universe
deb http://security.ubuntu.com/ubuntu/ focal-security multiverse
# deb-src http://security.ubuntu.com/ubuntu/ focal-security multiverse

```

服务器列表

| 服务商              | 地址                                     |
| ------------------- | ---------------------------------------- |
| Ubuntu 官方         | `http://archive.ubuntu.com/ubuntu/`        |
| Ubuntu 官方（中国） | `http://cn.archive.ubuntu.com/ubuntu/`     |
| 网易（广东广州）    | `http://mirrors.163.com/ubuntu/`          |
| 阿里云              | `http://mirrors.aliyun.com/ubuntu/`        |
| 腾讯                | `http://mirrors.cloud.tencent.com/ubuntu/` |
| 华为                | `http://mirrors.huaweicloud.com/ubuntu/`   |

## 将 zsh 设置为默认的 Shell

```bash
sudo apt install zsh

# zsh设为默认shell

chsh -s /bin/zsh

# 安装 git、curl
sudo apt install git curl

# 安装oh-my-zsh
sh -c "$(curl -fsSL https://raw.github.com/robbyrussell/oh-my-zsh/master/tools/install.sh)"
```

- [Ubuntu 超炫的 ZSH 配置](https://zhuanlan.zhihu.com/p/27052046)
- [Ubuntu | 安装oh-my-zsh](https://www.jianshu.com/p/ba782b57ae96)

### 设置代理

```bash
# 临时代理
export http_proxy=127.0.0.1:10809
export https_proxy=$http_proxy

# 修改配置文件，使用快捷指令启用
vim ~/.zshrc

# proxy
alias setproxy='export http_proxy=127.0.0.1:1087;export https_proxy=$http_proxy'
alias unsetproxy='unset http_proxy;unset https_proxy'

source ~/.zshrc
```

### 更新 oh-my-zsh

```bash
upgrade_oh_my_zsh
```

## 安装 OpenSSH

```bash
sudo apt-get install openssh-server

# 启动 OpenSSH
sudo /etc/init.d/ssh start
```

设置账号的缺省身份标识

```bash
git config --global user.email "you@example.com"
git config --global user.name "Your Name"
```

## 安装 Docker

添加软件源

```bash
sudo apt-get install \
    apt-transport-https \
    ca-certificates \
    curl \
    software-properties-common

curl -fsSL https://mirrors.ustc.edu.cn/docker-ce/linux/ubuntu/gpg | sudo apt-key add -

sudo add-apt-repository \
    "deb [arch=amd64] https://mirrors.ustc.edu.cn/docker-ce/linux/ubuntu \
    $(lsb_release -cs) \
    stable"
```

安装 Docker CE

```bash
sudo apt-get install docker-ce
```

启用 Docker CE

```bash
sudo systemctl enable docker
sudo systemctl start docker
```

建立 docker 用户组

```bash
sudo groupadd docker
sudo usermod -aG docker $USER
```

镜像加速

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

[清理 Docker 文件](https://www.cnblogs.com/yogoup/p/12143103.html)

```bash
docker system prune
```

## 安装 PostgreSql

安装 Docker 镜像版

```bash
docker pull postgres:latest
docker run --name postgres -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres:latest
```

启动 postgres

```bash
docker container start postgres
```

## 安装 .Net SDK

```bash
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1
```

[安装教程](https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-ubuntu)

## 安装 Nodejs

```bash
sudo apt install nodejs
sudo apt install npm
```

安装 NodeJs 管理工具

```bash
npm install -g n
```

## 安装 TypeScript

```bash
sudo npm install -g typescript
tsc -v
```

[安装教程](https://classic.yarnpkg.com/zh-Hans/docs/install#debian-stable)

## 安装 Whistle

```bash
sudo npm install -g whistle
```

[whistle 教程](https://wproxy.org/whistle/)

## 安装 Yarn

```bash
curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | sudo apt-key add -
echo "deb https://dl.yarnpkg.com/debian/ stable main" | sudo tee /etc/apt/sources.list.d/yarn.list

sudo apt-get update && sudo apt-get install yarn

yarn --version
```

## 安装 Nginx

安装依赖包

```bash
sudo apt install gcc libpcre3-dev zlib1g-dev openssl libssl-dev make
```

- `gcc`: C 语言编译器
- `libpcre3-dev`: 正则表达式库
- `zlib1g-dev`: 数据压缩库
- `openssl`: TLS/SSL 加密库
- `make`: 解释 Makefile 的命令工具

下载 [Nginx](https://nginx.org/en/download.html)，选择 Stable version

解压 Nginx 压缩包

```bash
tar -zxvf nginx-1.18.0.tar.gz
```

执行 Nginx 默认配置

```bash
cd nginx-1.18.0/
./configure
```

执行编译、安装

```bash
# 编译
make

# 安装
sudo make install
```

启动 Nginx

```bash
cd /usr/local/nginx/sbin/
sudo ./nginx
```

验证 Nginx 是否成功启动

```bash
# 查看 Nginx 进程
ps -ef | grep nginx # 如果显示了 Nginx 的进程，说明已经成功启动

# 请求 80 端口
curl http://127.0.0.1 # 如果看到了 Welcome to nginx!，说明已经成功启动
```
