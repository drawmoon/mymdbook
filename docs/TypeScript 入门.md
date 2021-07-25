# TypeScript 入门

- [Npm 包管理与 NodeJs 版本管理](#npm-包管理与-nodejs-版本管理)
- [语法与风格检查](#语法与风格检查)
- [为模块扩展属性与方法](#为模块扩展属性与方法)
- [回调函数](#回调函数)
- [策略模式消除 if-else 的方法](#策略模式消除-if-else-的方法)

## Npm 包管理与 NodeJs 版本管理

### 查看全局安装依赖包

```bash
npm list -g --depth 0
```

### Npm 依赖包升级

安装

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

## 语法与风格检查

安装

```bash
npm install --save-dev eslint @typescript-eslint/parser @typescript-eslint/eslint-plugin prettier eslint-config-prettier eslint-plugin-prettier
```

- `eslint`: `ESLint` 核心代码
- `@typescript-eslint/parser`: `TypeScript` 解析器
- `@typescript-eslint/eslint-plugin`: `ESLint` 插件
- `prettier`: `Prettier` 核心代码
- `eslint-config-prettier`: 用于关闭所有不必要的或可能与 `Prettier` 冲突的规则
- `eslint-plugin-prettier`: `Prettier` 的 `ESLint` 插件


创建 ESLint 配置文件 `.eslintrc.js`：

```javascript
module.exports = {
  parser: '@typescript-eslint/parser',
  parserOptions: {
    sourceType: 'module',
  },
  plugins: ['@typescript-eslint/eslint-plugin'],
  extends: [
    'plugin:@typescript-eslint/recommended',
    'prettier',
    'plugin:prettier/recommended',
  ],
  root: true,
  env: {
    node: true,
    jest: true,
  }
};
```

- `parser`: 指定 ESLint 解析器
- `parserOptions`: 指定 JavaScript 语言选项，默认情况下需要 ECMAScript 5 语法，可以配置 ECMAScript 其他版本或 JSX 的支持
- `plugins`: 配置第三方插件
- `extends`: 表示继承基础配置中的规则
- `root`: 默认情况下，ESLint 会在父目录中寻找配置文件，一直到根目录；如果指定为 `true`，就不会在父目录中寻找
- `env`: 指定脚本的运行环境

创建 Prettier 配置文件 `.prettierrc`：

```json
{
  "singleQuote": true,
  "trailingComma": "all"
}
```

- `singleQuote`: 使用单引号替代双引号
- `trailingComma`: 在对象或数组的最后一个元素后面加上逗号

配置 `packgae.json`：

```json
{
  "scripts": {
    "lint": "eslint \"{src,test}/**/*.ts\" --fix"
  }
}
```

## 为模块扩展属性与方法

### 为模块扩展属性

如果需要扩展其他模块，需要新建类型声明文件，下面新建 `typing.d.ts` 文件，文件名称可以根据模块名来命名 `express.d.ts`：

```typescript
export {};

declare module 'express' {
  interface Request {
    items: Record<string, string>;
  }
}
```

接下来可以在引入模块后使用该属性：

```typescript
import { Request } from 'express';

const req: Request;
const items = req.items;
```

### 为模块扩展方法

在 `typing.d.ts` 文件中声明方法：

```typescript
export {};

declare module 'express' {
  interface Request {
    items: Record<string, string>;

    getItem(key: string): string;

    setItem(key: string, value: string): void;
  }
}
```

新建 `express-extensions.ts` 文件：

```typescript
import * as express from 'express';

express.request.getItem = function (key: string): string {
  if (this.items) {
    return this.items[key];
  }
  return undefined;
};

express.request.setItem = function (key: string, value: string): void {
  if (this.items) {
    this.items[key] = value;
  }
  this.items = { [key]: value };
};
```

引入文件后使用扩展方法：

```typescript
import { Request } from 'express';
import './express-extensions';

import * as express from 'express';
const app = express;

const req: Request = app.request;

req.setItem('key1', 'value1');
const value = req.getItem('key1');
```

## 回调函数

```typescript
doSome(callback: (id: string) => string) {
}

this.doSome((id) => {
  
});
```

泛型回调函数

```typescript
doSome(callback: <T>(id: T) => string) {
}

this.doSome((id) => {
  
});
```

## 策略模式消除 if-else 的方法

```typescript
remove<T>(type: string, id: T) {
  if (type === 'file') {

  } else if (type === 'folder') {

  } else if (type === 'tag') {
    
  } else {
    
  }
}
```

```typescript
private readonly relationRecord: Record<string, <TKey>(id: TKey) => Promise<void>> = {};
  
constructor() {
  this.initRelationRecord();
}

private initRelationRecord(): void {
  this.relationRecord['folder'] = async (id) => {
    
  };

  this.relationRecord['file'] = async (id) => {
    
  };

  this.relationRecord['tag'] = async (id) => {
    
  };
}

async remove<T>(type: string, id: T): Promise<void> {
  await this.relationRecord[type](id);
}
```
