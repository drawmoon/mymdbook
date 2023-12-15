import * as express from 'express';

const app = express();

// 实现一个简单的中间件
app.use((req: express.Request, res: express.Response, next: express.NextFunction) => {
  const start = Date.now();

  console.log(`开始处理请求 '${req.method} ${req.url}'`);

  next();

  const end = Date.now();
  console.log(`处理请求 '${req.method} ${req.url}' 结束 +${end - start} ms`);
});

app.get('/', (req: express.Request, res: express.Response) => {
  console.log('开始处理接口逻辑');

  res.send('Hello, World!');

  console.log('处理接口逻辑结束');
});

app.listen(3000, () => console.log('Now listening on: http://localhost:3000.'));
