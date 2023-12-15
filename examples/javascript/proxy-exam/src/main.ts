import * as express from 'express';
import { createProxyMiddleware } from 'http-proxy-middleware';

const app = express();

// middlewares
app.use((req, res, next) => {
  const { path } = req;

  if (/^\/api\/\d.\d\/(?:foo|bar)/.test(path)) {
    next();
  } else {
    const proxy = createProxyMiddleware(path, {
      target: 'http://localhost:8000',
      changeOrigin: true,
    });

    proxy(req, res, next);
  }
});

// apis
app.get('/api/1.0/foo', (req, res) => {
  const { path } = req;

  res.send(`You visited: ${path}, Current datetime: ${new Date().toLocaleString()}`);
});

app.get('/api/1.0/bar', (req, res) => {
  const { path } = req;

  res.send(`You visited: ${path}, Current datetime: ${new Date().toLocaleString()}`);
});

const port = 3000;
app.listen(port, () => console.log('Now listening on: http://localhost:%d.', port));
