import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';
import * as tf from '@tensorflow/tfjs-node';
import { LayersModel } from '@tensorflow/tfjs-node';
dotenv.config();

const app: Express = express();
const port = process.env.PORT;
let model: LayersModel;

(async ()=>{
  model = await tf.loadLayersModel(tf.io.fileSystem("./load-model/model.json"));
})();

app.get('/:age', async (req: Request, res: Response) => {
  const { age } = req.params;
  const tensor = tf.tensor(age.split(",").map(n => parseInt(n)));
  const predictions = await model.predict(tensor);
  res.send(predictions);
});

app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at http://localhost:${port}`);
});
