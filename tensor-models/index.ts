import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';
import * as tf from '@tensorflow/tfjs-node';
dotenv.config();

const app: Express = express();
const port = process.env.PORT;

const t = tf.tensor([[1, 2, 3], [1, 2, 3]]);

app.get('/', (req: Request, res: Response) => {
    tf.ones([4,4]).print();

  res.send('Express + TypeScript Server');
});

app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at http://localhost:${port}`);
});
