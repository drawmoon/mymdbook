interface Commodity {
  id: string;
  type: string;
}

const comms: Commodity[] = [];
comms.push({ id: '1', type: 'chart' });

const item = { id: '1', type: 'chart' };

const index = comms.indexOf(item);
console.log(index); // -1
