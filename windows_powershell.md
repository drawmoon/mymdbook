# Windows PowerShell

设置执行策略：

```shell
Set-ExecutionPolicy AllSigned
```

在 `$HOME\Documents\PowerShell` 目录下新建 `Microsoft.PowerShell_profile.ps1` 配置文件：

```shell
# =====================================================================================
# Proxy config start.
# How to use:
# ```powershell
# PS > SetProxy
# PS > RemoveProxy
# ```
# =====================================================================================
function SetProxy {
    $proxy='http://127.0.0.1:10809'
    $ENV:HTTP_PROXY=$proxy
    $ENV:HTTPS_PROXY=$proxy
    Write-Host "设置代理:" $proxy
}
function RemoveProxy {
    $proxy=$ENV:HTTP_PROXY
    Remove-Item -Path "Env:\HTTP_PROXY" -Force
    Remove-Item -Path "Env:\HTTPS_PROXY" -Force
    Write-Host "移除代理:" $proxy
}
# =====================================================================================
# Proxy config end.
# =====================================================================================


# =====================================================================================
# PSReadLine config start.
# =====================================================================================
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
# =====================================================================================
# PSReadLine config end.
# =====================================================================================
```
