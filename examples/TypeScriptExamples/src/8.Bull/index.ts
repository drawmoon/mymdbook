import * as Queue from 'bull';
import { sleep } from 'sleep';

const queue = new Queue('f');

queue.process((job, done) => {
  console.log(job, done);
  const { i, seq } = job.data;
  sleep(i);
  console.log('process', seq);
});

let seq = 0;
for (let i = 5; i > 0; i--) {
  seq++;
  queue.add({ i: i, seq: seq });
}
