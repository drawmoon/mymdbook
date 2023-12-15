/*
  Use `console.time`
 */
async function logTime() {
  console.time('Total elapsed time');

  for (let i = 0; i < 1000; i++) {
    await new Promise<void>((r) => {
      setTimeout(r, 1);
    });
  }

  console.timeEnd('Total elapsed time');
}

logTime().then();

/*
  Use `Stopwatch`
 */
class Stopwatch {
  private total = 0;
  private startTime;

  constructor() {
    this.startTime = Date.now();
  }

  public start(): void {
    this.startTime = Date.now();
  }

  public stop(): void {
    this.total += Date.now() - this.startTime;
  }

  public get ms(): number {
    return this.total;
  }
}

async function logTime2() {
  const totalElapsedTime = new Stopwatch();

  for (let i = 0; i < 1000; i++) {
    await new Promise<void>((r) => {
      setTimeout(r, 1);
    });
  }

  totalElapsedTime.stop();
  console.log(`Total elapsed time': ${totalElapsedTime.ms} ms`);
}

logTime2().then();
