# 代码检查

安装

```shell
npm install --save-dev eslint @typescript-eslint/parser @typescript-eslint/eslint-plugin typescript
```

- `eslint`: `ESLint` 核心代码
- `@typescript-eslint/parser`: `TypeScript` 解析器
- `@typescript-eslint/eslint-plugin`: `ESLint` 插件

创建 `ESLint` 的配置文件，配置文件命名为 `.eslintrc.js`：

```javascript
'use strict';

module.exports = {
  parser: '@typescript-eslint/parser',
  parserOptions: {
    ecmaVersion: 2020,
    sourceType: 'script',
    project: './tsconfig.json'
  },
  plugins: ['@typescript-eslint'],
  extends: ['plugin:@typescript-eslint/recommended']
};
```

- `parser`: 指定 `ESLint` 解析器
- `parserOptions`: 指定 `JavaScript` 语言选项，默认情况下需要 `ECMAScript 5` 语法，可以配置 `ECMAScript` 其他版本或 `JSX` 的支持
- `plugins`: 配置第三方插件
- `extends`: 表示继承基础配置中的规则

配置 `packgae.json`：

```json
{
    "scripts": {
        "lint": "eslint . --ext .ts"
    }
}
```