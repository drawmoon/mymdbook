import * as express from 'express';
import './express-plugin';

const app = express();

app.get('/', (req: express.Request, res: express.Response) => {
  req.setItem('msg', 'Hello, World!');

  const msg = req.getItem('msg');

  res.send(msg);
});

app.listen(3000, () => console.log('Now listening on: http://localhost:3000.'));
