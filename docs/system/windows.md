# Windows 10 Notes

- [美化 PowerShell](#美化-powershell)
  - [自动补全](#自动补全)
  - [语法高亮显示](#语法高亮显示)
- [WSL](#wsl)
  - [systemctl](#systemctl)
  - [zsh](#zsh)
  - [WSL GUI](#wsl-gui)
  - [WSL 使用 Windows 的代理](#wsl-使用-windows-的代理)
- [截图与贴图工具](#截图与贴图工具)

## WSL

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

### systemctl

### zsh

### WSL GUI

### WSL 使用 Windows 的代理

如果代理客户端使用的是 v2rayN，首先需要配置 v2rayN ☑ 允许来自局域网的连接。

```bash
export hostip=$(cat /etc/resolv.conf |grep -oP '(?<=nameserver\ ).*')
export https_proxy="http://${hostip}:10809"
export http_proxy="http://${hostip}:10809"
```

### 补充

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
