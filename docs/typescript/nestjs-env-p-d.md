# NestJs 中的多环境的配置

安装依赖包

```bash
npm install --save dotenv

# Or
yarn add dotenv
```

新建`.env`、`.development.env`文件，用于生产环境与开发环境

修改`package.json`

```json
{
  "scripts": {
    "start": "node -r dotenv/config \"./node_modules/@nestjs/cli/bin/nest.js\" start dotenv_config_path=.development.env",
    "start:dev": "node --watch -r dotenv/config \"node_modules/@nestjs/cli/bin/nest.js\" start dotenv_config_path=.development.env",
    "start:prod": "node main",
    "start:inspect": "node --inspect=127.0.0.1:9229 main"
  }
}
```
