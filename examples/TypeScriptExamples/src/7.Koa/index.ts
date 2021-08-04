import * as Koa from 'koa';
import * as Router from 'koa-router';

const app = new Koa();
const router = new Router();

router.get('/', (ctx) => {
  ctx.body = 'Hello World';
});

app.use(router.routes());
app.listen(3000, () => {
  console.log('Now listening on: http://localhost:3000.');
});
