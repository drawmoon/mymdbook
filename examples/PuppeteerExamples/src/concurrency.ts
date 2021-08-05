import { Cluster } from 'puppeteer-cluster';

async function main() {
  const cluster = await Cluster.launch({
    maxConcurrency: 2,
    concurrency: Cluster.CONCURRENCY_CONTEXT,
    puppeteerOptions: {
      headless: false,
      args: ['--no-sandbox', '--disable-setuid-sandbox'],
    }
  })
}
