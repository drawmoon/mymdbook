# 在 WSL 中使用 Docker Desktop

先决条件

- Windows 10 版本 1903 或更高的版本
- 在 Windows 上启用 WSL 2
- 在 Docker Desktop 中 **Settings > General**，已勾选 `Use the WSL 2 based engine`
- 确保在 Windows 中将 WSL 2 设置为默认版本，执行 `wsl -l -v` 查看所有的分发

转到 Docker Desktop **Settings > Resources > WSL Integration**，选择你想启用的 Linux 发行版

![docker settings](../images/docker-wsl-integration.png)

点击 **Apply & Restart** 重新启动 Docker Desktop 后，即可在 WSL 中使用 Docker 命令

完成以上步骤后，在 WSL 中没有用 `sudo` 执行 Docker 命令时会提示权限不足，需要将当前用户添加到 Docker 用户组

```bash
sudo usermod -aG docker $USER
```
