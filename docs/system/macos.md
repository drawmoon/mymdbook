# macOS Notes

- [MacOS 下的软件包的管理器](#macos-下的软件包的管理器)
- [将 zsh 设置为默认的 Shell](#将-zsh-设置为默认的-shell)
- [配置 oh-my-zsh](#配置-oh-my-zsh)
- [适用于 oh-my-zsh 的自动建议插件](#适用于-oh-my-zsh-的自动建议插件)
- [适用于 oh-my-zsh 的语法高亮显示插件](#适用于-oh-my-zsh-的语法高亮显示插件)
- [在终端中配置全局代理](#在终端中配置全局代理)
- [MacOS 下的终端神器](#macos-下的终端神器)

## MacOS 下的软件包的管理器

> Homebrew - macOS（或 Linux）缺失的软件包的管理器

在终端中输入命令执行安装

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install.sh)"
```

对于下载比较慢的问题，如果你不使用代理，可以更换为国内源。

```bash
cd "$(brew --repo)"
git remote set-url origin git://mirrors.ustc.edu.cn/brew.git
cd "$(brew --repo)/Library/Taps/homebrew/homebrew-core"
git remote set-url origin git://mirrors.ustc.edu.cn/homebrew-core.git
```

## 将 zsh 设置为默认的 Shell

如果系统默认使用的是 bash，在终端中执行`cat /etc/shells`列出支持的`Shell`。

执行`chsh -s /bin/zsh`切换为 zsh。

## 配置 [oh-my-zsh](https://github.com/ohmyzsh/ohmyzsh)

安装 Git

```bash
brew install git
```

执行以下命令自动安装 oh-my-zsh

```bash
sh -c "$(wget -O- https://raw.githubusercontent.com/ohmyzsh/ohmyzsh/master/tools/install.sh)"

# 或者，先将脚本文件下载下来，确保没有问题后执行脚本文件
wget https://raw.githubusercontent.com/ohmyzsh/ohmyzsh/master/tools/install.sh
sh install.sh
```

## 适用于 oh-my-zsh 的[自动建议插件](https://github.com/zsh-users/zsh-autosuggestions)

克隆仓库到 oh-my-zsh 的插件目录

```bash
git clone https://github.com/zsh-users/zsh-autosuggestions ~/.oh-my-zsh/custom/plugins/zsh-autosuggestions
```

编辑`.zshrc`文件，添加`zsh-autosuggestions`到`plugins`

```bash
vim ~/.zshrc

# 进入编辑模式，添加 zsh-autosuggestions 并保存
plugins=([plugins...] zsh-autosuggestions)
```

重新启动终端

## 适用于 oh-my-zsh 的[语法高亮显示插件](https://github.com/zsh-users/zsh-syntax-highlighting)

克隆仓库到 oh-my-zsh 的插件目录

```bash
git clone https://github.com/zsh-users/zsh-syntax-highlighting.git ${ZSH_CUSTOM:-~/.oh-my-zsh/custom}/plugins/zsh-syntax-highlighting
```

编辑`.zshrc`文件，添加`zsh-syntax-highlighting`到`plugins`

```bash
vim ~/.zshrc

# 进入编辑模式，添加 zsh-syntax-highlighting 并保存
plugins=([plugins...] zsh-syntax-highlighting)
```

## 在终端中配置全局代理

```bash
# 如果没有配置 oh-my-zsh，执行 vim ~/.bashrc
vim ~/.zshrc

# 进入编辑模式，添加这两行代码并保存
alias setproxy='export http_proxy=127.0.0.1:10808;export https_proxy=$http_proxy'
alias unsetproxy='unset http_proxy;unset https_proxy'

source ~/.zshrc

# 启用代理
setproxy

# 停止代理
unsetproxy
```

## MacOS 下的终端神器

> iTerm2 是 Terminal 的替代品，是 iTerm 的后继产品。它适用于装有 macOS 10.14 或更高版本的 Mac。 iTerm2 将终端带入了您从未想过一直想要的功能，使其进入了现代时代。

[Download iTerm2](https://www.iterm2.com/index.html)
