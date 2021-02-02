# macOS 开发环境使用

系统版本：macOS。

## iTerm2

[Home](https://www.iterm2.com/index.html) \
[安装教程](https://www.jianshu.com/p/ba08713c2b19)

将 zsh 用作默认 Shell

在终端中执行`cat /etc/shells`列出支持的`Shell`。

执行`chsh -s /bin/zsh`。

[设置代理教程](https://my.oschina.net/Jerrymingzj/blog/805769)

```bash
vim ~/.zshrc

# proxy
alias setproxy='export http_proxy=127.0.0.1:1087;export https_proxy=$http_proxy'
alias unsetproxy='unset http_proxy;unset https_proxy'

source ~/.zshrc
```

执行`setproxy`开启代理，执行`unsetproxy`停止代理。

## Homebrew

在`iTerm2`中输入命令执行安装。

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

## Git

利用`Homebrew`安装。

```bash
brew install git
```

## Nodejs

[官网](https://nodejs.org/)

## Yarn

利用`Homebrew`安装。

```bash
brew install yarn
```

## TypeScript

利用`npm`安装。

```bash
npm install -g typescript
```

[whistle 教程](https://wproxy.org/whistle/)

## Docker

利用`Homebrew`安装。

```bash
brew cask install docker
```

[安装教程](https://yeasy.gitbook.io/docker_practice/install/mac)

[清理 Docker 文件](https://www.cnblogs.com/yogoup/p/12143103.html)

```bash
docker system prune
```

## whistle

> 基于Node实现的跨平台web调试代理工具。

利用`npm`安装。

```bash
npm install -g whistle
```

## 截图/贴图

[官网](https://zh.snipaste.com)
