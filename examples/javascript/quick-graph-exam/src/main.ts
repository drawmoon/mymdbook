import { BidirectionalGraph, Edge, TopologicalSortAlgorithm } from 'quick-graph-ts';
import { captureId, isExtend, QueryField } from './query';

let qfs: QueryField[] = [
  { id: 1, name: 'stud_cnt', expression: 'count({24})', cachedData: 41 },
  { id: 2, name: 'teach_cnt', expression: 'count({38})', cachedData: 16 },
  { id: 3, name: 'cnt', expression: '$1 - $2' },
  { id: 4, name: 'ord_cnt', expression: 'count({2})', cachedData: 121 },
  { id: 5, name: 'cnt2', expression: '$3 + $4' },
  { id: 6, name: 'cnt3', expression: '$5 - $3' },
  { id: 7, name: 'cnt4', expression: '($5 - $3) * $2 + 1' },
];
// shuffle elements
qfs = qfs.sort(() => Math.random() - 0.5);
console.log('fetch all query-field, id:', qfs.map((q) => q.id).join(','));

function bootstrap() {
  console.log(3, fetch(3)); // 3 25
  console.log(5, fetch(5)); // 5 146
  console.log(6, fetch(6)); // 6 121
  console.log(7, fetch(7)); // 7 1937
}

function fetch(id: number): number | undefined {
  const qf = qfs.find((q) => q.id === id);

  if (!qf) return undefined;

  if (qf.cachedData) {
    return qf.cachedData;
  }

  if (isExtend(qf)) {
    return fetchExtendQueryFieldData(qf);
  }

  // ...
  return undefined;
}

function fetchExtendQueryFieldData(qf: QueryField): number {
  const g = initGraph(qf, qfs);

  const tsa = new TopologicalSortAlgorithm(g);
  tsa.compute();

  const dict: { [key: string]: number } = {};

  const sdvs = tsa.sortedVertices;
  console.log('sorted vertices, id:', sdvs.map((qf) => qf.id).join(','));

  for (const v of sdvs.filter((q) => q != qf)) {
    if (!v.cachedData) {
      if (isExtend(v)) {
        v.cachedData = fetchExtendQueryFieldData(v);
      } else {
        // ...
        console.log('fetching data,', v.id);
      }
    }

    dict[`$${v.id}`] = v.cachedData!;
  }

  let expr = qf.expression;
  for (const key in dict) {
    expr = expr.replace(key, dict[key].toString());
  }

  return eval(expr);
}

function initGraph(crt: QueryField, all: QueryField[]): BidirectionalGraph<QueryField, Edge<QueryField>> {
  const kin = findKin(crt, all);
  const qfs = [crt, ...kin];

  const g = new BidirectionalGraph<QueryField, Edge<QueryField>>();

  const dict: { [key: number]: QueryField } = {};

  for (const qf of qfs) {
    dict[qf.id] = qf;
    g.addVertex(qf);
  }

  for (const qf of qfs.filter((q) => isExtend(q))) {
    const pids = captureId(qf);
    const ps = pids.map((pid) => dict[pid]);

    for (const p of ps) {
      g.addEdge(new Edge<QueryField>(qf, p));
    }
  }

  return g;
}

function findKin(crt: QueryField, all: QueryField[]): QueryField[] {
  const pids = captureId(crt);

  if (pids.length === 0) return [];

  const kin: QueryField[] = [];

  for (const pid of pids) {
    const qf = all.find((q) => q.id === pid);
    if (qf) {
      kin.push(qf, ...findKin(qf, all));
    }
  }

  return kin;
}

bootstrap();
