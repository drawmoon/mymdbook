# Windows 10 终端美化、智能提示

- [美化 PowerShell](#美化-powershell)
- [自动补全、语法高亮显示](#自动补全、语法高亮显示)

## 美化 PowerShell

查看 PowerShell 版本

```powershell
$PSVersionTable
```

下载最新版的 [PowerShell](https://github.com/PowerShell/PowerShell/releases)，下载 [Windows Terminal](https://www.microsoft.com/zh-cn/p/windows-terminal/9n0dx20hk701?activetab=pivot:overviewtab)

打开 Windows Terminal，按`CTRL + ,`进入设置 -> 启动 -> 默认配置文件，选择 PowerShell，设置为默认的 Shell

安装 oh-my-posh

```powershell
Install-Module oh-my-posh -Scope AllUsers
```

下载 [Meslo LGM NF](https://github.com/ryanoasis/nerd-fonts/releases/download/v2.1.0/Meslo.zip) 字体，按`CTRL + ,`进入设置 -> 打开 JSON 文件，设置默认字体

```json
{
    "profiles":
    {
        "defaults":
        {
            "fontFace": "MesloLGM NF"
        }
    }
}
```

获取所有主题

```powershell
Get-PoshThemes
```

使用记事本编辑 Profile

```powershell
notepad $Profile
```

添加以下内容

```conf
# oh-my-posh
Import-Module oh-my-posh
# 设置主题为 stelbent.minimal
Set-PoshPrompt -Theme stelbent.minimal
```

## [自动补全、语法高亮显示](https://github.com/PowerShell/PSReadLine)

安装 PSReadLine

```powershell
Install-Module -Name PSReadLine -AllowPrerelease -Force
```

安装 posh-git

```powershell
Install-Module posh-git -Scope AllUsers
```

使用记事本编辑 Profile

```powershell
notepad $Profile
```

添加以下内容

```conf
# PSReadLine
Import-Module PSReadLine
# 设置预测文本来源为历史记录
Set-PSReadLineOption -PredictionSource History
# 设置 Tab 键补全
Set-PSReadlineKeyHandler -Key Tab -Function Complete
# 设置 Ctrl+d 为菜单补全和 Intellisense
Set-PSReadLineKeyHandler -Key "Ctrl+d" -Function MenuComplete
# 设置 Ctrl+z 为撤销
Set-PSReadLineKeyHandler -Key "Ctrl+z" -Function Undo
# 设置向上键为后向搜索历史记录
Set-PSReadLineKeyHandler -Key UpArrow -Function HistorySearchBackward
# 设置向下键为前向搜索历史纪录
Set-PSReadLineKeyHandler -Key DownArrow -Function HistorySearchForward
# posh-git
Import-Module posh-git
```
