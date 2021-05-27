# Ubuntu 终端美化、智能提示

安装`zsh`，并设置为默认的`shell`

```bash
sudo apt install zsh

# 将 zsh 设为默认 shell
chsh -s /bin/zsh
```

## oh-my-zsh

```bash
# 安装 git、curl
sudo apt install git curl

# 安装 oh-my-zsh
sh -c "$(curl -fsSL https://raw.github.com/robbyrussell/oh-my-zsh/master/tools/install.sh)"
```

## [语法高亮](https://github.com/zsh-users/zsh-syntax-highlighting)

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

## [历史记录](https://github.com/zsh-users/zsh-autosuggestions)

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

## 自动补全

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

> 参阅： [Ubuntu | 安装 oh-my-zsh](https://www.jianshu.com/p/ba782b57ae96)
