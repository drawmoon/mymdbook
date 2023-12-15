# aws-test

## 安装与构建

```shell
yarn install && yarn run build
```

## 启动

修改 `.env` 中的配置：

- `ENDPOINT`: 端点，示例: `localhost:9000`
- `USE_SSL`: 是否启用 SSL
- `ACCESS_KEY`: 访问密钥
- `SECRET_ACCESS_KEY`: 秘密访问密钥
- `REGION`: 区域
- `BUCKET_NAME`: 存储桶名称，不存在则新建存储桶

```shell
yarn run start
```

## 如何测试

上载文件:

```shell
curl -X POST http://localhost:3000/assets
```

> 会将项目根目录的 `assets` 目录下的 `iphone.png` 与 `public.zip` 文件上载至服务器

下载文件:

```shell
curl -X GET http://localhost:3000/assets
```

> 会将上载的 `iphone.png` 与 `public.zip` 文件从服务器中下载至项目根目录的 `downloads` 目录下 
