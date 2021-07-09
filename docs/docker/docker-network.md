# Docker 网络

网络服务是 Docker 提供的一种容器互联的方式。

## 列出所有网络

```bash
docker network ls
```

## 创建网络

```bash
docker network create <网络>
```

`-d` 可以指定网络的类型，分别有 `bridge`、`overlay`。

```bash
docker network create -d bridge <网络>
```

## 删除网络

```bash
docker network rm <网络>
```

## 连接容器

`--network` 将容器连接到 `my-network` 网络。

```bash
docker run --network my-network --name pg-server -d postgres

docker run --network my-network --name my-web -e DB_HOST=pg-server -d web
```
