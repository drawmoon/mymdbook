# Ubuntu Notes

- [修改源](#修改源)
- [zsh](#zsh)
  - [oh-my-zsh](#oh-my-zsh)
  - [语法高亮](#语法高亮)
  - [历史记录](#历史记录)
  - [自动补全](#自动补全)
  - [更新 oh-my-zsh](#更新-oh-my-zsh)
  - [设置代理](#设置代理)
- [安装 NodeJs](#安装-nodejs)
- [用 Npm 安装 Yarn](#利用-npm-安装-yarn)
- [安装 Nginx](#安装-nginx)

## 修改源

服务器列表

| 服务商              | 地址                                       |
| ------------------- | ------------------------------------------ |
| Ubuntu 官方         | `http://archive.ubuntu.com/ubuntu/`        |
| Ubuntu 官方（中国） | `http://cn.archive.ubuntu.com/ubuntu/`     |
| 网易（广东广州）    | `http://mirrors.163.com/ubuntu/`           |
| 阿里云              | `http://mirrors.aliyun.com/ubuntu/`        |
| 腾讯                | `http://mirrors.cloud.tencent.com/ubuntu/` |
| 华为                | `http://mirrors.huaweicloud.com/ubuntu/`   |

```bash
# 安装 vim，或使用 vi
sudo apt install vim

# 修改文件
sudo vim /etc/apt/source.list

# 按 i 键进入编辑模式，编辑完成后按 Esc 键退出编辑模式，按 : 键，然后输入 wq 回车保存并退出
```

示例

```bash
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

## zsh

安装`zsh`，并设置为默认的`shell`

```bash
sudo apt install zsh

# 将 zsh 设为默认 shell
chsh -s /bin/zsh
```

### oh-my-zsh

```bash
# 安装 git、curl
sudo apt install git curl

# 安装 oh-my-zsh
sh -c "$(curl -fsSL https://raw.github.com/robbyrussell/oh-my-zsh/master/tools/install.sh)"
```

### [语法高亮](https://github.com/zsh-users/zsh-syntax-highlighting)

将仓库克隆到`plugins`目录

```bash
cd ~/.oh-my-zsh/plugins && git clone https://github.com/zsh-users/zsh-syntax-highlighting.git
```

修改`zsh`配置文件

```bash
vim ~/.zshrc
```

在`plugins`中添加`zsh-syntax-highlighting`，需要注意`zsh-syntax-highlighting`必须添加在最后一个，例如：

```conf
plugins=(git zsh-syntax-highlighting)
```

### [历史记录](https://github.com/zsh-users/zsh-autosuggestions)

将仓库克隆到`plugins`目录

```bash
cd ~/.oh-my-zsh/plugins && git clone https://github.com/zsh-users/zsh-autosuggestions
```

修改`zsh`配置文件

```bash
vim ~/.zshrc
```

在`plugins`中添加`zsh-autosuggestions`，例如

```conf
plugins=(git zsh-autosuggestions)
```

### 自动补全

下载插件，并将插件放在`plugins`目录下

```bash
# 创建目录
mkdir -p ~/.oh-my-zsh/plugins/incr && cd ~/.oh-my-zsh/plugins/incr
# 下载插件
wget https://mimosa-pudica.net/src/incr-0.2.zsh
```

修改`zsh`配置文件

```bash
vim ~/.zshrc
```

在文件末尾添加以下内容

```conf
# incr
source ~/.oh-my-zsh/plugins/incr/incr*.zsh
```

刷新配置

```bash
source ~/.zshrc
```

> 参阅： [Ubuntu | 安装 oh-my-zsh](https://www.jianshu.com/p/ba782b57ae96)

### 更新 oh-my-zsh

```bash
upgrade_oh_my_zsh
```

### 设置代理

```bash
# 修改配置文件，使用快捷指令启用
sudo vim ~/.zshrc

# proxy
alias setproxy='export http_proxy=127.0.0.1:10809;export https_proxy=$http_proxy'
alias unsetproxy='unset http_proxy;unset https_proxy'

source ~/.zshrc

# 启用代理
setproxy

# 禁用代理
unsetproxy
```

## 安装 NodeJs

下载[Linux 二进制文件](https://nodejs.org/en/download/)

```bash
VERSION=v14.16.1
DISTRO=linux-x64
sudo mkdir -p /usr/local/lib/nodejs
sudo tar -xJvf node-$VERSION-$DISTRO.tar.xz -C /usr/local/lib/nodejs
```

设置环境变量

```bash
sudo vim ~/.zshrc

# 添加到末尾

# Nodejs
VERSION=v14.16.1
DISTRO=linux-x64
export PATH=/usr/local/lib/nodejs/node-$VERSION-$DISTRO/bin:$PATH
```

保存后执行`source ~/.zshrc`刷新`.zshrc`

## 用 Npm 安装 Yarn

```bash
sudo npm install -g yarn
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
