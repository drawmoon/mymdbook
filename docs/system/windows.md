# Windows 10 Notes

- [美化 PowerShell](#美化-powershell)
  - [自动补全、语法高亮显示](#自动补全、语法高亮显示)
- [WSL](#wsl)
  - [systemctl](#systemctl)
  - [zsh](#zsh)
    - [oh-my-zsh](#oh-my-zsh)
    - [语法高亮](#语法高亮)
    - [历史记录](#历史记录)
    - [自动补全](#自动补全)
  - [WSL GUI](#wsl-gui)
  - [WSL 使用 Windows 的代理](#wsl-使用-windows-的代理)
- [截图与贴图工具](#截图与贴图工具)

## 美化 PowerShell

查看 PowerShell 版本

```powershell
$PSVersionTable
```

下载最新版的 [PowerShell](https://github.com/PowerShell/PowerShell/releases)，下载 [Windows Terminal](https://www.microsoft.com/zh-cn/p/windows-terminal/9n0dx20hk701?activetab=pivot:overviewtab)

打开 Windows Terminal，按`CTRL + ,`进入设置 -> 启动 -> 默认配置文件，选择 PowerShell，设置为默认的 Shell

安装 oh-my-posh

```powershell
Install-Module oh-my-posh -Scope AllUsers
```

下载 [Meslo LGM NF](https://github.com/ryanoasis/nerd-fonts/releases/download/v2.1.0/Meslo.zip) 字体，按`CTRL + ,`进入设置 -> 打开 JSON 文件，设置默认字体

```json
{
    "profiles":
    {
        "defaults":
        {
            "fontFace": "MesloLGM NF"
        }
    }
}
```

获取所有主题

```powershell
Get-PoshThemes
```

使用记事本编辑 Profile

```powershell
notepad $Profile
```

添加以下内容

```conf
# oh-my-posh
Import-Module oh-my-posh
# 设置主题为 aliens
Set-PoshPrompt -Theme aliens
```

## [自动补全、语法高亮显示](https://github.com/PowerShell/PSReadLine)

安装 PSReadLine

```powershell
Install-Module -Name PSReadLine -AllowPrerelease -Force
```

安装 posh-git

```powershell
Install-Module posh-git -Scope AllUsers
```

使用记事本编辑 Profile

```powershell
notepad $Profile
```

添加以下内容

```conf
# PSReadLine
Import-Module PSReadLine
# 设置预测文本来源为历史记录
Set-PSReadLineOption -PredictionSource History
# 设置 Tab 键补全
Set-PSReadlineKeyHandler -Key Tab -Function Complete
# 设置 Ctrl+d 为菜单补全和 Intellisense
Set-PSReadLineKeyHandler -Key "Ctrl+d" -Function MenuComplete
# 设置 Ctrl+z 为撤销
Set-PSReadLineKeyHandler -Key "Ctrl+z" -Function Undo
# 设置向上键为后向搜索历史记录
Set-PSReadLineKeyHandler -Key UpArrow -Function HistorySearchBackward
# 设置向下键为前向搜索历史纪录
Set-PSReadLineKeyHandler -Key DownArrow -Function HistorySearchForward
# posh-git
Import-Module posh-git
```

## WSL

启用适用于 Linux 的 Windows 的子系统

```powershell
dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart
```

安装 WSL2 之前，必须启用`虚拟机平台`

```powershell
dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart
```

命令执行完成后，重新启动计算机

下载并安装 [WSL2](https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi)

设置 WSL2 为默认版本

```powershell
wsl --set-default-version 2
```

在 Microsoft Store 中安装 [Ubuntu 20.04 LTS](https://www.microsoft.com/zh-cn/p/ubuntu-2004-lts/9n6svws3rx71#activetab=pivot:overviewtab)

### [systemctl](https://github.com/DamionGans/ubuntu-wsl2-systemd-script)

安装 Git

```bash
sudo apt install git
```

克隆仓库，并执行脚本

```bash
git clone https://github.com/DamionGans/ubuntu-wsl2-systemd-script.git
cd ubuntu-wsl2-systemd-script/
bash ubuntu-wsl2-systemd-script.sh
```

脚本执行完成后重启 WSL

```bash
#停止LxssManager服务
net stop LxssManager
#启动LxssManager服务
net start LxssManager
```

验证 systemctl 是否工作

```bash
systemctl
```

### zsh

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

### WSL GUI

更新 WSL2

```powershell
wsl --update
```

执行完成后重启 WSL

```bash
#停止LxssManager服务
net stop LxssManager
#启动LxssManager服务
net start LxssManager
```

重启完成后，在 WSL 中安装 GUI app 后，在 Windows 的开始菜单会显示 WSL 中的 GUI app

### WSL 使用 Windows 的代理

如果代理客户端使用的是 v2rayN，首先需要配置 v2rayN ☑ 允许来自局域网的连接。

```bash
export hostip=$(cat /etc/resolv.conf |grep -oP '(?<=nameserver\ ).*')
export https_proxy="http://${hostip}:10809"
export http_proxy="http://${hostip}:10809"
```

## 截图与贴图工具

[Download Snipaste](https://zh.snipaste.com/)

## 安装 Yarn

更改 PowerShell 执行策略

```bash
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

```bash
npm install -g yarn
```
