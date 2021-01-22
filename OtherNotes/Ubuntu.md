# Ubuntu 开发环境使用

系统版本：Ubuntu 20.4 LTS。

## Vim

```
➜  ~ sudo apt install vim
```

## 修改源

```
# 备份文件
➜  ~ sudo cp /etc/apt/source.list source.list.backup

# 修改文件
➜  ~ sudo vim /etc/apt/source.list

# 将文件内容修改为以下内容，其中http://cn.archive.ubuntu.com/ubuntu/可以修改为下面服务器列表中的任意一个服务商地址
# See http://help.ubuntu.com/community/UpgradeNotes for how to upgrade to
# newer versions of the distribution.
deb http://cn.archive.ubuntu.com/ubuntu/ focal main restricted
# deb-src http://archive.ubuntu.com/ubuntu/ focal main restricted

## Major bug fix updates produced after the final release of the
## distribution.
deb http://cn.archive.ubuntu.com/ubuntu/ focal-updates main restricted
# deb-src http://archive.ubuntu.com/ubuntu/ focal-updates main restricted

## N.B. software from this repository is ENTIRELY UNSUPPORTED by the Ubuntu
## team. Also, please note that software in universe WILL NOT receive any
## review or updates from the Ubuntu security team.
deb http://cn.archive.ubuntu.com/ubuntu/ focal universe
# deb-src http://archive.ubuntu.com/ubuntu/ focal universe
deb http://cn.archive.ubuntu.com/ubuntu/ focal-updates universe
# deb-src http://archive.ubuntu.com/ubuntu/ focal-updates universe

## N.B. software from this repository is ENTIRELY UNSUPPORTED by the Ubuntu
## team, and may not be under a free licence. Please satisfy yourself as to
## your rights to use the software. Also, please note that software in
## multiverse WILL NOT receive any review or updates from the Ubuntu
## security team.
deb http://cn.archive.ubuntu.com/ubuntu/ focal multiverse
# deb-src http://archive.ubuntu.com/ubuntu/ focal multiverse
deb http://cn.archive.ubuntu.com/ubuntu/ focal-updates multiverse
# deb-src http://archive.ubuntu.com/ubuntu/ focal-updates multiverse

## N.B. software from this repository may not have been tested as
## extensively as that contained in the main release, although it includes
## newer versions of some applications which may provide useful features.
## Also, please note that software in backports WILL NOT receive any review
## or updates from the Ubuntu security team.
deb http://cn.archive.ubuntu.com/ubuntu/ focal-backports main restricted universe multiverse
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
| Ubuntu 官方         | http://archive.ubuntu.com/ubuntu/        |
| Ubuntu 官方（中国） | http://cn.archive.ubuntu.com/ubuntu/     |
| 网易（广东广州）    | http://mirrors.163.com/ubuntu/           |
| 阿里云              | http://mirrors.aliyun.com/ubuntu/        |
| 腾讯                | http://mirrors.cloud.tencent.com/ubuntu/ |
| 华为                | http://mirrors.huaweicloud.com/ubuntu/   |

## Git

```
➜  ~ sudo apt install git
```

设置账号的缺省身份标识

```
➜  ~ git config --global user.email "you@example.com"
➜  ~ git config --global user.name "Your Name"
```

## 将 zsh 用作默认 Shell

```
➜  ~ sudo apt install zsh

# zsh设为默认shell

➜  ~ chsh -s /bin/zsh

# 安装oh-my-zsh
➜  ~ sh -c "$(curl -fsSL https://raw.github.com/robbyrussell/oh-my-zsh/master/tools/install.sh)"
```

[教程](https://zhuanlan.zhihu.com/p/27052046)
[教程 2](https://www.jianshu.com/p/ba782b57ae96)

设置代理

```
# 临时代理
➜  ~ export http_proxy=127.0.0.1:10809
➜  ~ export https_proxy=127.0.0.1:10809

# 修改配置文件，使用快捷指令启用
➜  ~ vim ~/.zshrc

# proxy
alias setproxy='export http_proxy=127.0.0.1:1087;export https_proxy=$http_proxy'
alias unsetproxy='unset http_proxy;unset https_proxy'

➜  ~ source ~/.zshrc
```

手动更新`oh-my-zsh`

```
➜  ~ upgrade_oh_my_zsh
```

## Docker

添加软件源

```
➜  ~ sudo apt-get install \
    apt-transport-https \
    ca-certificates \
    curl \
    software-properties-common

➜  ~ curl -fsSL https://mirrors.ustc.edu.cn/docker-ce/linux/ubuntu/gpg | sudo apt-key add -

➜  ~ sudo add-apt-repository \
    "deb [arch=amd64] https://mirrors.ustc.edu.cn/docker-ce/linux/ubuntu \
    $(lsb_release -cs) \
    stable"
```

安装 Docker CE

```
➜  ~ sudo apt-get install docker-ce
```

启用 Docker CE

```
➜  ~ sudo systemctl enable docker
➜  ~ sudo systemctl start docker
```

建立 docker 用户组

```
➜  ~ sudo groupadd docker
➜  ~ sudo usermod -aG docker $USER
```

镜像加速

```
➜  ~ vim /etc/docker/daemon.json

{
  "registry-mirrors": [
    "https://hub-mirror.c.163.com",
    "https://mirror.baidubce.com"
  ]
}
```

重启服务

```
➜  ~ sudo systemctl daemon-reload
➜  ~ sudo systemctl restart docker
```

[安装教程](https://yeasy.gitbook.io/docker_practice/install/ubuntu)

[清理 Docker 文件](https://www.cnblogs.com/yogoup/p/12143103.html)

```
➜  ~ docker system prune
```

## PostgreSql

安装 Docker 镜像版

```
➜  ~ docker pull postgres:latest
➜  ~ docker run --name postgres -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres:latest
```

启动 postgres

```
➜  ~ docker container start postgres
```

## .Net Core SDK

```
➜  ~ wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
➜  ~ sudo dpkg -i packages-microsoft-prod.deb

➜  ~ sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1
```

[安装教程](https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-ubuntu)

## Nodejs

```
➜  ~ sudo apt install nodejs
➜  ~ sudo apt install npm
```

## TypeScript

```
➜  ~ sudo npm install -g typescript
➜  ~ tsc -v
```

## Yarn

```
➜  ~ curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | sudo apt-key add -
➜  ~ echo "deb https://dl.yarnpkg.com/debian/ stable main" | sudo tee /etc/apt/sources.list.d/yarn.list

➜  ~ sudo apt-get update && sudo apt-get install yarn

➜  ~ yarn --version
```

[安装教程](https://classic.yarnpkg.com/zh-Hans/docs/install#debian-stable)

## Whistle

```
➜  ~ sudo npm install -g whistle
```

[whistle 教程](https://wproxy.org/whistle/)
