# 学习笔记11: CI/CD

- [GitHub Actions](#github-actions)

## GitHub Actions

> GitHub Actions 是 GitHub 提供的仓库自动工作流程功能，用户可以利用 GitHub 提供的免费计算机资源轻松实现 CI/CD，还可以用来做很多有意思的事情。

### 创建一个 CI 工作流程

在项目根目录创建 `.github/workflows/ci.yml` 文件，Github 会自动执行 `workflows` 目录下的 `yml` 文件

```yaml
# 工作流名称
name: ci

# 工作流执行条件，push 或 push_request 时触发执行工作流
on: [ push, pull_request ]

# 或限制分支
# on:
#   push:
#     branches: [ main ]
#   pull_request:
#     branches: [ main ]

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
          # 缓存记录的 Key，还原时根据这个 Key 去搜索
          key: ${{ runner.os }}-build-${{ env.cache-name }}
          # 没有匹配到 Key 时，会根据 Restore—keys 的规则去匹配
          restore-keys: |
            ${{ runner.os }}-build-${{ env.cache-name }}-
            ${{ runner.os }}-build-
            ${{ runner.os }}-

      # 安装项目依赖
      - name: install dependencies
        run: npm install

      # linter
      - name: lint
        run: npm run lint

      # 构建项目
      - name: build
        run: npm run build

      # 执行测试
      - name: test
        run: npm run test
```

- [查看所有 Actions](https://github.com/marketplace?type=actions)
