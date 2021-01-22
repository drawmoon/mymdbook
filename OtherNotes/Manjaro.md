# Manjaro 开发环境使用

系统版本：Manjaro GNOME。

## 更新软件源

```bash
sudo pacman-mirrors -i -c China -m rank
sudo pacman -Syy
```

配置 archlinuxcn 软件源

[清华archlinuxcn软件源](https://mirror.tuna.tsinghua.edu.cn/help/archlinuxcn/)

```bash
sudo vim /etc/pacman.conf
```

```txt
[archlinuxcn]
SigLevel = Optional TrustedOnly
Server = Server = https://mirrors.tuna.tsinghua.edu.cn/archlinuxcn/$arch
```

更新密钥

```bash
sudo pacman -Syy && sudo pacman -S archlinuxcn-keyring
```

## Vim

```bash
sudo pacman -S vim
```

## yaourt

```bash
sudo pacman -S yaourt
```

更新软件源

```bash
yaourt mirrorlist # 1 2
```

## Proxy

```bash
sudo pacman -S v2ray
yaourt -S qv2ray
```

## Google 拼音输入法

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

## Git

```bash
sudo pacman -S git
```

## SmartGit

下载[压缩包](https://www.syntevo.com/smartgit/download/)并解压

```bash
tar xzf <smartgit*.tar.gz>
cd smartgit/bin
bash smartgit.sh
```

## Visual Studio Code

```bash
yaourt -S code
```

## dotnet sdk

```bash
yaourt -S dotnet-sdk-5.0
```

## nodejs

```bash
sudo pacman -S nodejs
```

## npm

```bash
sudo pacman -S npm
```

## yarn

```bash
sudo pacman -S yarn
```

## TypeScript

```bash
sudo pacman -S typescript
```

## 截图、贴图

```bash
sudo pacman -S flameshot
```

打开“设置”-“键盘快捷键”设置快捷键

```txt
名称：flameshot
命令：/usr/bin/flameshot gui
快捷键：PrtScn
```

## 远程桌面

```bash
pacman -S remmina freerdp libvncserver telepathy-glib gnome-keyring nxproxy spice-gtk3 xorg-server-xephyr
```

## Docker

安装 Docker

```bash
sudo pacman -S docker
```

启动 Docker 服务

```bash
sudo systemctl start docker
```

建立 Docker 用户组

```bash
sudo groupadd docker
```

将当前用户加入 Docker 组

```bash
sudo gpasswd -a ${USER} docker
```

退出当前终端并重新登录。

## PostgreSql

拉取 Docker 镜像并执行

```bash
docker pull postgres:latest
docker run --name postgres -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres:latest
```

启动 Postgres

```bash
docker container start postgres
```

## Whistle

```bash
npm i -g whistle
```
