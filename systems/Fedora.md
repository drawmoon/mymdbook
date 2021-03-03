# Fedora开发环境使用

系统版本：Fedora 33。

## Proxy 工具

1. 添加源

```bash
# dnf config-manager --add-repo https://download.opensuse.org/repositories/home:zzndb:Qv2ray/Fedora_33/home:zzndb:Qv2ray.repo
```

2. 安装

```bash
# dnf install Qv2ray
```

[qv2ray.net下载Qv2ray文档](https://qv2ray.net/getting-started/step1.html#opensuse-fedora-centos)

## Git

```bash
# dnf install git
```

设置账号的缺省身份标识

```
$ git config --global user.email "you@example.com"
$ git config --global user.name "Your Name"
```

## SmartGit

1. 下载压缩包，[官网](https://www.syntevo.com/smartgit/download/)
2. 解压压缩包

```bash
$ tar xzf <smartgit*.tar.gz>
```

3. 运行

```bash
$ cd smartgit/bin
# bash smartgit.sh
```

4. 添加到应用程序中

```bash
# bash add-menuitem.sh
```

## Visual Studio Code

1. 添加源和密钥

```bash
# rpm --import https://packages.microsoft.com/keys/microsoft.asc
# sh -c 'echo -e "[code]\nname=Visual Studio Code\nbaseurl=https://packages.microsoft.com/yumrepos/vscode\nenabled=1\ngpgcheck=1\ngpgkey=https://packages.microsoft.com/keys/microsoft.asc" > /etc/yum.repos.d/vscode.repo'
```

2. 安装

```bash
# dnf check-update
# dnf install code
```

## VirtualBox

1. 更改为`root`角色

```bash
$ sudo -i
```

2. 安装`rpmrebuild`

```bash
# dnf install rpmrebuild
```

3. 下载`VirtualBox`

```bash
# cd /tmp
# wget http://download.virtualbox.org/virtualbox/rpm/fedora/32/x86_64/VirtualBox-6.1-6.1.16_140961_fedora32-1.x86_64.rpm
```

4. 重新构建`VirtualBox`包

```bash
# rpmrebuild --change-spec-preamble='sed -e "s/6.1.16_140961_fedora32/6.1.16_140961_fedora33/"' --change-spec-requires='sed -e "s/python(abi) = 3.8/python(abi) >= 3.8/"' --package VirtualBox-6.1-6.1.16_140961_fedora32-1.x86_64.rpm
```

5. 安装依赖包

```bash
# dnf install binutils gcc make patch libgomp glibc-headers glibc-devel kernel-headers kernel-devel dkms qt5-qtx11extras libxkbcommon
```

6. 安装`VirtualBox`

```bash
# dnf install ~/rpmbuild/RPMS/x86_64/VirtualBox-6.1-6.1.16_140961_fedora33-1.x86_64.rpm
```

7. 将`VirtualBox`用户添加到`vboxusers`组

```bash
# usermod -a -G vboxusers <user_name>
```

## VMware Player

1. [官网](https://www.vmware.com/products/workstation-player.html)下载
2. 安装依赖包

```bash
# dnf install kernel kernel-headers kernel-devel
```

3. 执行安装

```bash
# sh VMware-Player-16.0.0-16894299.x86_64.bundle
```

4. 运行`VMware Player`

```bash
$ vmplayer
```

卸载`VMware Player`

```bash
# vmware-installer -u vmware-player
```

如果出现`kernel`相关错误，执行以下命令清理旧的内核

查看当前正在使用的内核版本

```bash
$ uname -r
```

查看所有版本的内核

```bash
$ rpm -qa|grep kernel
```

删除指定内核

```bash
$ sudo -i
# yum remove <kernel>
```

## BT 下载工具

```bash
# dnf install transmission
```

