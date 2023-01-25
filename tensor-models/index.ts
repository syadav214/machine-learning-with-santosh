import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';
import * as tf from '@tensorflow/tfjs-node';
import { LayersModel, Tensor } from '@tensorflow/tfjs-node';
dotenv.config();

const app: Express = express();
const port = process.env.PORT;
let model: LayersModel;

(async ()=>{
  model = await tf.loadLayersModel(tf.io.fileSystem("./load-model/model.json"));
})();

app.get('/:age', async (req: Request, res: Response) => {
  const result = [];
  try {
    const { age } = req.params;
    const data = age.split(",").map(n => parseInt(n));
    tf.util.shuffle(data);

    const inputTensor = tf.tensor2d(data, [data.length, 1]);
    const inputMax = inputTensor.max();
    const inputMin = inputTensor.min();  
    const normalizedInputs = inputTensor.sub(inputMin).div(inputMax.sub(inputMin));

    const predictions = model.predict(normalizedInputs.reshape([-1,1]), { batchSize: 4 }) as tf.Tensor;
    const predArr = predictions.arraySync() as [];

    for(let i = 0;i < data.length;i++){
      const d = predArr[i] as number[];
      result.push({
        age: data[i],
        canLive: Math.round(d[0]) === 1,
        canDie: Math.round(d[1]) === 1,
      })
    }
  } catch(e) {
    console.log("e", e)
  }
  res.send(result);
});

app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at http://localhost:${port}`);
});
