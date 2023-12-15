# Ubuntu

## 使用 APT 安装

```shell
sudo apt-get update

sudo apt-get install \
    nodejs \
    npm
```

接下来执行以下命令安装 TypeScript 编译器：

```shell
npm install -g typescript
```

测试安装：

```shell
tsc --version
```