# Rider 远程调试

File > Settings > Build, Execution, Deployment > Debugger > Remote Debug

![Remote Debug](../assets/rider-remote-debug1.png)

添加 SSH Session

```txt
Host: localhost Prot: 22
User name: root
Authentication type: Password
Password: root
```

![Add SSH Session](../assets/rider-remote-debug2.png)

Run > Attach To Remote Process...

安装 Remote Debug Tools

![Install Remote Debug Tools](../assets/rider-remote-debug3.png)

选择对应的进程

![Select PID](../assets/rider-remote-debug4.png)
