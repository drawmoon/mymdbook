# Windows 10 WSL

- [安装 WSL2](#安装-wsl2)
- [zsh](#zsh)
  - [oh-my-zsh](#oh-my-zsh)
  - [语法高亮](#语法高亮)
  - [历史记录](#历史记录)
- [在 Windows 中启动 WSL 图形应用](#在-windows-中启动-wsl-图形应用)
- [在 Windows 中启动 WSL 桌面](#在-windows-中启动-wsl-桌面)
- [systemctl](#systemctl)
- [WSL 使用 Windows 的代理](#wsl-使用-windows-的代理)

## 安装 WSL2

启用 WSL：

```powershell
dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart
```

启用 `虚拟机平台`：

```powershell
dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart
```

> 以上命令执行完成后重启计算机。

下载安装 [WSL2](https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi)，设置 WSL2 为默认版本：

```powershell
wsl --set-default-version 2
```

在 Microsoft Store 中获取 [Debian](https://www.microsoft.com/zh-cn/p/debian/9msvkqc78pk6?activetab=pivot:overviewtab)。

安装完成后，启动 Debian 输入新的用户名与密码后，WSL2 就已经安装完毕了。

建议将 Debian 修改为国内的软件源，执行 `vim /etc/apt/source.list` 命令，修改为以下内容：

```bash
deb http://mirrors.huaweicloud.com/debian buster main
deb http://mirrors.huaweicloud.com/debian buster-updates main
deb http://mirrors.huaweicloud.com/debian-security/ buster/updates main
deb http://mirrors.huaweicloud.com/debian buster-backports main
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

刷新配置

```bash
source ~/.zshrc
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

刷新配置

```bash
source ~/.zshrc
```

## 在 Windows 中启动 WSL 图形应用

> 使用 X 协议在 Windows 中渲染 Linux 应用图形界面。这里需要借助 MobaXterm 工具，并启用 X server。

安装 Xorg

```bash
sudo apt install xorg -y
```

配置将图形显示到 X server 所在的机器上：

```bash
sudo vim /etc/profile.d/x.sh
```

将以下内容输入到 `x.sh` 中：

```bash
export DISPLAY=$(grep -m 1 nameserver /etc/resolv.conf | awk '{print $2}'):0.0
```

刷新配置

```bash
source /etc/profile
```

以上步骤完成后，即可在 WSL 中启动图形界面应用。

## 在 Windows 中启动 WSL 桌面

> 使用 X 协议在 Windows 中渲染 Linux 应用图形界面。这里需要借助 MobaXterm 工具，并启用 X server。

切换为中文环境；

```bash
sudo dpkg-reconfigure locales

# Locales to be generated:
# zh_CN .UTF-8 UTF-8

# Default locale for the system environment:
# en_US.UTF-8
```

```bash
sudo apt update
```

安装字体管理包

```bash
sudo apt-get install fontconfig -y
```

安装中文字体

```bash
sudo mkdir -p /usr/share/fonts/windows
sudo cp -r /mnt/c/Windows/Fonts/*.ttf /usr/share/fonts/windows/
```

清除字体缓存

```bash
fc-cache
```

生成中文环境

```bash
sudo locale-gen zh_CN.UTF-8
```

安装中文输入法

```bash
sudo apt install fcitx dbus-x11 fcitx-googlepinyin -y
```

配置用户环境变量：

```bash
sudo vim /etc/profile.d/fcitx.sh

export GTK_IM_MODULE=fcitx
export QT_IM_MODULE=fcitx
export XMODIFIERS=@im=fcitx
```

安装 LXDE 桌面应用：

```bash
sudo apt install lxde -y
```

启动 LXDE：

```bash
lxsession
```

你也可以尝试安装其他的桌面应用：

```bash
# xfce4
sudo apt install xfce4 xfce4-goodies -y
# 启动
startxfce4

# Gnome
sudo apt install gnome ibus-rime -y
# 选择 gdm3
# 启动
gnome-shell

# KDE
sudo apt install kde-plasma-desktop -y
# 选择 sddm
# 启动
startkde
```

## [systemctl](https://github.com/DamionGans/ubuntu-wsl2-systemd-script)

安装 Git

```bash
sudo apt install git
```

执行以下命令，克隆仓库并执行脚本：

```bash
git clone https://github.com/DamionGans/ubuntu-wsl2-systemd-script.git
cd ubuntu-wsl2-systemd-script/
bash ubuntu-wsl2-systemd-script.sh
```

脚本执行完成后重启 WSL：

```bash
wsl --shutdown
```

验证 systemctl 是否工作：

```bash
systemctl
```

> 每次重新启动 Windows 后需要重新执行脚本。

## WSL 使用 Windows 的代理

如果代理客户端使用的是 v2rayN，首先需要配置 v2rayN ☑ 允许来自局域网的连接。

### 临时配置代理

```bash
export hostip=$(cat /etc/resolv.conf |grep -oP '(?<=nameserver\ ).*')
export https_proxy="http://${hostip}:10809"
export http_proxy="http://${hostip}:10809"
```

### 编写脚本配置代理

```bash
# 修改配置文件，使用快捷指令启用
sudo vim ~/.zshrc
# or
# sudo vim ~/.profile

# proxy
alias setproxy='export hostip=$(cat /etc/resolv.conf |grep -oP '(?<=nameserver\ ).*');export http_proxy=${hostip}:10809;export https_proxy=$http_proxy'
alias unsetproxy='unset http_proxy;unset https_proxy'

source ~/.zshrc
# or
# source ~/.profile

# 启用代理
setproxy

# 禁用代理
unsetproxy
```
