# Windows 10 WSL GUI

- [在 Windows 中启动 WSL 图形应用](#在-windows-中启动-wsl-图形应用)
- [在 Windows 中启动 WSL 桌面](#在-windows-中启动-wsl-桌面)

## 在 Windows 中启动 WSL 图形应用

> 使用 X 协议在 Windows 中渲染 Linux 应用图形界面。这里需要借助 MobaXterm 工具，并启用 X server。

安装 Xorg

```bash
sudo apt install xorg -y
```

使用 MobaXterm 连接 WSL，即可在 WSL 中启动图形界面应用。

如果遇到使用 MobaXterm 启动应用太慢的情况，可以尝试用以下方式启动应用：

- 下载并安装 Windows Terminal
- 打开 MobaXterm，点击首页的 Start local terminal
- 查看终端上方显示的 `Your DISPLAY is set to 192.168.1.1:0.0` 文字
- 在 Windows Terminal 中连接 WSL，并输入 `export DISPLAY=192.168.1.1:0.0`
- 随后就可以在 Windows Terminal 启动 WSL 图形界面应用

## 在 Windows 中启动 WSL 桌面

> 使用 X 协议在 Windows 中渲染 Linux 应用图形界面。这里需要借助 MobaXterm 工具，并启用 X server。

切换为中文环境：

```bash
sudo dpkg-reconfigure locales

# Locales to be generated:
# en_US.UTF-8 UTF-8
# zh_CN.UTF-8 UTF-8

# Default locale for the system environment:
# zh_CN.UTF-8
```

安装字体管理包

```bash
sudo apt update
sudo apt-get install fontconfig font-manager -y
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

> 如果遇到启动了 fcitx，并且已添加 Google 拼音输入法，却无法切换输入法，请尝试 [askubuntu](https://askubuntu.com/questions/1126451/unable-to-toggle-between-input-methods-using-fcitx) 上的回答解决该问题

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

> 需要注意的是，有些桌面应用依赖 `systemd`，可以参考 [WSL systemctl](windows-wsl.md#systemctl) 启用 `systemd`
