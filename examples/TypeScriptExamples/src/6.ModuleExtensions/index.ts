import { Request } from 'express';
import './express-extensions';

import * as express from 'express';
const app = express;

const req: Request = app.request;

req.setItem('key1', 'value1');
const value = req.getItem('key1');
console.log(value);
