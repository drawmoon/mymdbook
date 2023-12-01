import * as express from 'express';

const app = express();

app.get('/', (req, res) => {
  res.send('Hello, World!');
});

const port = 3000;
app.listen(port, () => console.log('Now listening on: http://localhost:%d.', port));
