# Npm Notes

- [安装 Npm](#安装-npm)
- [查看全局安装依赖包](#查看全局安装依赖包)
- [安装 NodeJs 管理工具](#安装-nodejs-管理工具)
- [安装 Npm 依赖包升级工具](#安装-npm-依赖包升级工具)
- [卸载 Npm](#卸载-npm)

## 安装 Npm

### Ubuntu

```bash
sudo apt install nodejs
sudo apt install npm
```

### Manjaro

```bash
sudo pacman -S nodejs
sudo pacman -S npm
```

## 查看全局安装依赖包

```bash
npm list -g --depth 0
```

## 安装 NodeJs 管理工具

```bash
npm install -g n
```

## 安装 Npm 依赖包升级工具

> 提供命令行图形界面，可以手动选择需要升级的依赖包

```bash
npm install -g npm-check
```

升级项目中的依赖包

```bash
npm-check -u

# 命令行图形界面，上下键可以移动选择，空格选中或取消选中
? Choose which packages to update. (Press <space> to select)

 Minor Update New backwards-compatible features.
>( ) typescript devDep  4.0.6  ❯  4.2.3  https://www.typescriptlang.org/

 Space to select. Enter to start upgrading. Control-C to cancel.
```

## 卸载 Npm

```bash
npm uninstall -g npm
```
