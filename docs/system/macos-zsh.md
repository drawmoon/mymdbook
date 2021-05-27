# macOS 终端美化、智能提示

将`zsh`设置为默认的`shell`

```bash
# 列出所有支持的 shell
cat /etc/shells
# 切换为 zsh
chsh -s /bin/zsh
```

下载并安装 [iTerm2](https://www.iterm2.com/index.html)，替代 macOS 内置的终端工具

安装 Homebrew

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install.sh)"
```

对于下载比较慢的问题，可以更换为国内源。

```bash
cd "$(brew --repo)"
git remote set-url origin git://mirrors.ustc.edu.cn/brew.git
cd "$(brew --repo)/Library/Taps/homebrew/homebrew-core"
git remote set-url origin git://mirrors.ustc.edu.cn/homebrew-core.git
```

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

## [自动补全](https://github.com/zsh-users/zsh-autosuggestions)

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

## [语法高亮显示](https://github.com/zsh-users/zsh-syntax-highlighting)

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
