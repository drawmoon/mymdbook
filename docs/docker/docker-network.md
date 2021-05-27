# Docker Network

## 查看所有网络

```bash
docker network ls
```

## 创建桥接网络

```bash
docker network create tmpnet
```

## 删除网络

```bash
docker network rm 1b
```

## 同一台机器上多个容器之间相互通信

将容器放在同一网络上，Docker 会自动 DNS 解析容器名称到 IP 地址

```bash
docker network create tmpnet
```

然后修改应用的配置，将机器的 IP 更改为容器的名称。示例：

```bash
docker network create tmpnet

docker run --net tmpnet --name pg_server -d postgres

docker run --net tmpnet --name myapp -e DB_HOST=pg_server -d myapp
# Or
# docker run --net tmpnet --name myapp -e DB_CONN_SER="Server=pg_server;Port=5432;" -d myapp
```
