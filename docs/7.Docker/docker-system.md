# Docker System

## 删除所有孤立的容器

```bash
docker container prune
```

## 清理构建缓存

```bash
docker builder prune
```

## 查看 Docker 的磁盘使用

```bash
docker system df
```

## 清理磁盘

删除关闭的容器、无用的数据卷、网络和构建缓存

```bash
docker system prune
```
