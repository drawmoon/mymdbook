# Visual Studio Code 插件与设置

## 设置

```
"editor.tabSize": 2,
"editor.formatOnSave": true
```

## 插件

### Markdownlint

### Markdown PDF

### TabNine

### Visual Studio IntelliCode

### ESLint

```
"editor.codeActionsOnSave": {
  "source.fixAll.eslint": true
}
```

### ~~TSLint~~

[TSLint 未来将被弃用，对 TypeScript 的支持将迁移至 ESLint](https://medium.com/palantir/tslint-in-2019-1a144c2317a9)

```
"editor.codeActionsOnSave": {
  "source.fixAll.tslint": true
}
```

### Prettier

将`Prettier`设置为默认代码格式化程序

```
"editor.defaultFormatter": "esbenp.prettier-vscode"
```

### Manta's Stylus Supremacy

```
"stylusSupremacy.insertColons": false,
"stylusSupremacy.insertSemicolons": false,
"stylusSupremacy.insertBraces": false,
"stylusSupremacy.insertNewLineAroundImports": false,
"stylusSupremacy.insertNewLineAroundBlocks": false
```

### language-stylus

```
"languageStylus.useSeparator": true,
"languageStylus.useBuiltinFunctions": true,
"editor.colorDecorators": true
```
