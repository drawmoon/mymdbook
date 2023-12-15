const regex = /\$(?<id>\d+)/gm;

export class QueryField {
  id: number;
  name: string;
  expression: string;
  cachedData?: number | undefined;
}

export function isExtend(qf: QueryField): boolean {
  try {
    return regex.test(qf.expression);
  } finally {
    regex.lastIndex = 0;
  }
}

export function captureId(qf: QueryField): number[] {
  try {
    const expr = qf.expression;

    const id: number[] = [];

    let arr: RegExpExecArray | null;
    while ((arr = regex.exec(expr)) != null) {
      id.push(Number(arr[1]));
    }

    return id;
  } finally {
    regex.lastIndex = 0;
  }
}
