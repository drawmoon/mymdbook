# docker compose

## up

启动并运行

```bash
docker-compose up
```

指定容器

```bash
docker-compose up -d nginx
```

后台运行

```bash
docker-compose up -d
```

指定 docker-compose 配置文件

```bash
docker-compose -f ./docker-compose.yml up
```

## down

删除所有容器

```bash
docker-compose down
```

## ps

显示所有容器

```bash
docker-compose ps
```

## logs

查看容器的日志

```bash
docker-compose logs nginx
```

实时日志

```bash
docker-compose logs nginx -f
```

## exec

进入到容器中

```bash
docker-compose exec nginx bash
```

## start

```bash
docker-compose start
```

## restart

重新启动容器

```bash
docker-compose restart nginx
```

## stop

停止正在运行的容器

```bash
docker-compose stop nginx
```

## pause

暂停容器

```bash
docker-compose pause nginx
```

## unpause

恢复容器

```bash
docker-compose unpause nginx
```

## rm

删除容器

```bash
docker-compose rm nginx
```

## build

构建镜像

```bash
docker-compose build nginx
```

不使用缓存

```bash
docker-compose build --no-cache nginx
```
