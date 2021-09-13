import { Request, Response } from 'express';
import './express-extensions';

import express = require('express');
const app: express.Application = express();

app.get('/', (req: Request, res: Response) => {
  req.setItem('key', 'World');
  const val = req.getItem('key');

  res.send(`Hello ${val}!`);
});

app.listen(3000, () => {
  console.log('Now listening on: http://localhost:3000.');
});
