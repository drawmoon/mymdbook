import * as Koa from 'koa';
import * as Router from 'koa-router';

const app = new Koa();
const router = new Router();

// 实现一个简单的中间件
app.use(async (ctx: Koa.Context, next: Koa.Next) => {
  const start = Date.now();

  console.log(`开始处理请求 '${ctx.req.method} ${ctx.req.url}'`);

  await next();

  const end = Date.now();
  console.log(`处理请求 '${ctx.req.method} ${ctx.req.url}' 结束 +${end - start} ms`);
});

router.get('/', (ctx: Koa.Context) => {
  console.log('开始处理接口逻辑');

  ctx.body = 'Hello, World!';

  console.log('处理接口逻辑结束');
});

app.use(router.routes());
app.listen(3000, () => console.log('Now listening on: http://localhost:3000.'));
