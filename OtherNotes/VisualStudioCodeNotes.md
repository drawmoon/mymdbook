# Table of contents

- [Visual Studio Code Notes](#visual-studio-code-notes)
  - [设置制表符缩进为 2 个空格](#设置制表符缩进为-2-个空格)
  - [编辑远程服务器文件](#编辑远程服务器文件)

# Visual Studio Code Notes

## 设置制表符缩进为 2 个空格

```json
{
  "editor.tabSize": 2
}
```

## 编辑远程服务器文件

> Install Remote - SSH

F1 > Remote-SSH: Connect to Host...

输入用户名与服务器地址

```conf
# Select configured SSH host or enter user@host

root@localhost
```

选择服务器类型

```conf
# Select the platform of the remote host "localhost"

> Linux
  Windows
  macOS
```

输入服务器密码

```conf
# Enter password for root@localhost

******
```

显示 Connected to remote 后，点击 Open Folder，输入路径就可以编辑文件了
