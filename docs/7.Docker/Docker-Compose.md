# Docker Compose

Docker Compose 可以通过编写 `docker-compose.yml` 模板文件，来定义一组相关联的容器为一个项目。

## docker-compose.yml

```yml
version: "3"
services:

  db:
    image: postgres
    container_name: pg-server
    # volumes: 
    #   - /app/ioea/data/pgdata:/var/lib/postgresql-static/data
    environment:
      POSTGRES_PASSWORD: postgres
      # PGDATA: /var/lib/postgresql-static/data
    ports:
      - "5432:5432"
    restart: always

  obs:
    image: minio/minio
    container_name: minio-server
    # volumes:
    #    - /tmp/data:/tmp/data
    environment:
      MINIO_ACCESS_KEY: AKIAIOSFODNN7EXAMPLE
      MINIO_SECRET_KEY: wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
    ports:
      - "9000:9000"
    command: server /tmp/data
    restart: always

  ioea:
    image: ioea
    container_name: ioea-server
    environment:
      DATABASE_HOST: pg-server
      DATABASE_PORT: 5432
      DATABASE_USERNAME: postgres
      DATABASE_PASSWORD: postgres
      DATABASE_NAME: ioea-db
      MINIO_END_POINT: minio-server
      MINIO_PORT: 9000
      MINIO_ROOT_USER: AKIAIOSFODNN7EXAMPLE
      MINIO_ROOT_PASSWORD: wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
      MINIO_USE_SSL: "false"
    depends_on:
      - db
      - obs
    restart: always

  nginx:
    image: nginx:latest
    container_name: nginx-server
    volumes:
      - /app/ioea/conf/nginx.conf:/etc/nginx/nginx.conf
      # - /app/ioea/wwwroot:/usr/share/nginx/html
    ports:
      - "80:80"
    depends_on:
      - db
      - obs
      - ioea
    restart: always
```

## 启动项目

自动构建镜像、创建网络、创建并启动服务。

```bash
docker-compose up
```

`-d` 可以使项目在后台运行

```bash
docker-compose up -d
```

指定 docker-compose 配置文件

```bash
docker-compose -f docker-compose.yml up
```

指定容器

```bash
docker-compose up -d <服务>
```

## 停止项目

停止并删除网络、服务。

```bash
docker-compose down
```

## 列出项目所有的容器

```bash
docker-compose ps
```

## 查看服务的输出

```bash
docker-compose logs <服务>
```

`-f` 监听服务的输出

```bash
docker-compose logs <服务> -f
```

## 进入到服务中

```bash
docker-compose exec <服务> /bin/bash
```

## 启动服务

```bash
docker-compose start <服务>
```

## 重新启动服务

```bash
docker-compose restart <服务>
```

## 停止正在运行的服务

```bash
docker-compose stop <服务>
```

## 暂停服务

```bash
docker-compose pause <服务>
```

## 恢复服务

```bash
docker-compose unpause <服务>
```

## 删除服务

```bash
docker-compose rm <服务>
```

## 构建服务

```bash
docker-compose build <服务>
```

`--no-cache` 不使用缓存

```bash
docker-compose build --no-cache <服务>
```
