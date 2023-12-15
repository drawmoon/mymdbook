# 依赖包管理

使用 `npm-check` 升级项目中的依赖包，在项目的 `package.json` 所在的目录下执行以下命令：

使用 `npx`：

```bash
npx npm-check -u
```

或执行 `npm install -g npm-check` 将 `npm-check` 安装到系统全局后再执行：

```bash
npm-check -u
```

等待工具检查依赖完毕后，会输出可以更新的依赖，按照推荐和谨慎来分开显示；按上下键可以移动选择，空格选中或取消选中，按下回车键后启动更新：

```bash
? Choose which packages to update. (Press <space> to select)

 Minor Update New backwards-compatible features.
>( ) typescript devDep  4.0.6  ❯  4.2.3  https://www.typescriptlang.org/

 Space to select. Enter to start upgrading. Control-C to cancel.
```

> 执行 `npm-check -u -g` 可以对系统全局的依赖进行升级