# 用 ESLint、Prettier 规范 TypeScript 代码

安装依赖项

```bash
npm install --save-dev eslint @typescript-eslint/parser @typescript-eslint/eslint-plugin prettier eslint-config-prettier eslint-plugin-prettier

# - eslint                            # ESLint 核心代码
# - @typescript-eslint/parser         # TypeScript 解析器
# - @typescript-eslint/eslint-plugin  # ESLint 插件
# - prettier                          # Prettier 核心代码
# - eslint-config-prettier            # 用于关闭所有不必要的或可能与 Prettier 冲突的规则
# - eslint-plugin-prettier            # Prettier 的 ESLint 插件
```

创建 ESLint 配置文件`.eslintrc.js`

```javascript
// .eslintrc.js

module.exports = {
  parser: '@typescript-eslint/parser',                // 指定 ESLint 解析器
  parserOptions: {                                    // 指定 JavaScript 语言选项，默认情况下需要 ECMAScript 5 语法，可以配置 ECMAScript 其他版本或 JSX 的支持
    sourceType: 'module',
  },
  plugins: ['@typescript-eslint/eslint-plugin'],      // 配置第三方插件
  extends: [                                          // 表示继承基础配置中的规则
    'plugin:@typescript-eslint/recommended',
    'prettier',
    'plugin:prettier/recommended',
  ],
  root: true,                                         // 默认情况下，ESLint 会在父目录中寻找配置文件，一直到根目录；如果指定为 true，就不会在父目录中寻找
  env: {                                              // 指定脚本的运行环境
    node: true,
    jest: true,
  }
};
```

创建 Prettier 配置文件`.prettierrc`

```json
// .prettierrc

{
  "singleQuote": true,      /* 使用单引号替代双引号 */
  "trailingComma": "all"    /* 在对象或数组的最后一个元素后面加上逗号 */
}
```

配置`packgae.json`中的`scripts`

```json
// package.json

{
  // ...
  "scripts": {
    "lint": "eslint \"{src,test}/**/*.ts\" --fix",
    // ...
  }
  // ...
}
```
