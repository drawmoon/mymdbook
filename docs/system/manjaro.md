# Manjaro Notes

- [修改软件源为国内源，添加 archlinuxcn 软件源](#修改软件源为国内源添加-archlinuxcn-软件源)
- [安装 Google 拼音输入法](#安装-google-拼音输入法)
- [V2ray Linux Web 客户端](#v2ray-linux-web-客户端)
- [截图与贴图工具](#截图与贴图工具)
- [远程桌面](#远程桌面)
- [安装 Visual Studio Code](#安装-visual-studio-code)
- [安装 DBeaver](#安装-dbeaver)
- [.NET SDK 5.0](#net-sdk)

## 修改软件源为国内源，添加 archlinuxcn 软件源

```bash
sudo pacman-mirrors -i -c China -m rank
sudo pacman -Syy
```

添加 archlinuxcn 软件源

[清华archlinuxcn软件源](https://mirror.tuna.tsinghua.edu.cn/help/archlinuxcn/)

```bash
sudo vim /etc/pacman.conf
```

```conf
[archlinuxcn]
SigLevel = Optional TrustedOnly
Server = https://mirrors.tuna.tsinghua.edu.cn/archlinuxcn/$arch
```

更新密钥

```bash
sudo pacman -Syy && sudo pacman -S archlinuxcn-keyring
```

## yaourt

```bash
sudo pacman -S yaourt
```

更新软件源

```bash
yaourt mirrorlist # 1 2
```

## 安装 Google 拼音输入法

```bash
sudo pacman -S fcitx-lilydjwg-git
sudo pacman -S fcitx-googlepinyin
sudo pacman -S fcitx-configtool fcitx-qt5
```

配置用户环境变量

```bash
vim ~/.pam_environment

GTK_IM_MODULE=fcitx
QT_IM_MODULE=fcitx
XMODIFIERS=@im=fcitx
```

最后重启计算机即可，如果重启计算机后任无法使用，输入以下命令卸载`fcitx`与依赖包

```bash
sudo pacman -Rsn $(pacman -Qsq fcitx)
```

然后重启计算机后重复上面的动作

## V2ray Linux Web 客户端

### 从软件源安装

```bash
# 安装打包工具
sudo pacman -S base-devel

# 克隆项目
git clone https://aur.archlinux.org/v2raya.git
# 开始构建项目
makepkg -si
```

部署成功后，访问`http://localhost:2017`进入到主界面

启动/停止服务

```bash
# 开机自启/取消自启/启动服务/停止服务/重启服务/查看状态
sudo systemctl enable/disable/start/stop/restart/status v2raya
```

### 在 Docker 中运行

```bash
docker run -d \
	--restart=always \
	--privileged \
	--network=host \
	--name v2raya \
	-v /etc/resolv.conf:/etc/resolv.conf \
	-v /etc/v2raya:/etc/v2raya \
	mzz2017/v2raya
```

部署成功后，访问`http://localhost:2017`进入到主界面

## 截图与贴图工具

```bash
sudo pacman -S flameshot
```

打开`设置 > 键盘快捷键`设置快捷键

```txt
名称：flameshot
命令：/usr/bin/flameshot gui
快捷键：PrtScn
```

## 远程桌面

```bash
pacman -S remmina \
	freerdp \
	libvncserver \
	telepathy-glib \
	gnome-keyring \
	nxproxy \
	spice-gtk3 \
	xorg-server-xephyr
```

## 安装 Visual Studio Code

```bash
yaourt -S visual-studio-code-bin
```

## 安装 DBeaver

```bash
sudo pacman -S dbeaver
```

## .NET SDK

```bash
yaourt -S dotnet-sdk-5.0
```

## Vim

```bash
sudo pacman -S vim
```

## Git

```bash
sudo pacman -S git
```

## Yarn

```bash
npm install -g yarn
```
