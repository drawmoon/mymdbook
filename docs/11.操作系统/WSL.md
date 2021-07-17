# Windows 10 WSL

- [安装 WSL2](#安装-wsl2)
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

脚本执行完成后重启 WSL，返回到 Windows 下，在终端中执行以下命令：

```bash
wsl --shutdown
```

验证 systemctl 是否工作：

```bash
systemctl
```

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
