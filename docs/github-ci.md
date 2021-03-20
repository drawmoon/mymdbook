# Github CI/CD

查看第三方提供的 [Actions](https://github.com/marketplace?type=actions)

创建`project/.github/workflows/ci.yml`文件，Github 会自动执行`workflows`目录下的`yml`文件

```yaml
# 简单的工作流配置示例

# 工作流名称
name: ci

# 工作流执行条件，push 或 push_request 到 main 分支时触发执行工作流
on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

# 定义工作流作业，可以定义多个作业
jobs:

  # 定义一个名称为 build 的作业
  build:

    # 作业的运行环境，可以是 Linux、MacOS 或 Windows
    runs-on: ubuntu-latest

    # 作业执行的阶段
    steps:

      # 拉取项目代码
      - uses: actions/checkout@v2

      # 设置为 Nodejs 环境
      - name: setup node
        uses: actions/setup-node@v1

      # 缓存依赖项
      - name: cache node modules
        uses: actions/cache@v2
        env:
          cache-name: cache-node-modules
        with:
          # 缓存的路径
          path: ~/.npm
          # 缓存记录的 Key
          key: ${{ runner.os }}-build-${{ env.cache-name }}
          restore-keys: |
            ${{ runner.os }}-build-${{ env.cache-name }}-
            ${{ runner.os }}-build-
            ${{ runner.os }}-

      # 安装项目依赖
      - name: install dependencies
        run: npm install

      # 代码格式化
      - name: format
        run: npm run format

      # 构建项目
      - name: build
        run: npm run build

      # 执行测试
      - name: test
        run: npm run test

```
