# 声明文件

## 模块扩展

如果需要扩展其他模块，需要新建类型声明文件，示例创建了 `typings/express.d.ts` 文件，并为 `Request` 扩展了 `items` 属性：

```typescript
declare module 'express' {
  interface Request {
    items: Record<string, string>;
  }
}
```

接下来可以在引入模块后使用该属性：

```typescript
import * as express from 'express';
import { Request, Response } from 'express';

const app = express();

app.get('/', (req: Request, res: Response) => {
  const items = req.items;
});
```

示例中为 `Request` 扩展了 `getItem` 与 `setItem` 函数：

```typescript
declare module 'express' {
  interface Request {
    items: Record<string, string>;

    getItem(key: string): string;

    setItem(key: string, value: string): void;
  }
}
```

新建 `express-plugin.ts` 文件：

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

引入文件后使用扩展函数：

```typescript
import * as express from 'express';
import { Request, Response } from 'express';
import './express-plugin';

const app = express();

app.get('/', (req: Request, res: Response) => {
  req.setItem('key', 'value');
  req.getItem('key');
});
```