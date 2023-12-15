import { Cluster } from 'puppeteer-cluster';
import { Page, BoundingBox } from 'puppeteer';

async function screenshot(
  cluster: Cluster,
  options: {
    url: string;
    clip?: BoundingBox | undefined;
    waitXpath?: string | undefined;
    headers?: Record<string, string> | undefined;
  }
): Promise<void> {
  const { url, clip, waitXpath, headers } = options;

  const task = async ({ page }: { page: Page }) => {
    try {
      if (headers) {
        await page.setExtraHTTPHeaders(headers);
      }

      const waitUntil = !waitXpath ? 'domcontentloaded' : 'load';
      await page.goto(url, { waitUntil: waitUntil });

      if (waitXpath) {
        await page.waitForXPath(waitXpath);
      }

      await page.screenshot({
        encoding: 'binary',
        type: 'png',
        fullPage: !clip,
        clip: clip,
      });
    } finally {
      // await page.close();
    }
  };

  const startTime = Date.now();

  await cluster.execute(task);

  console.log(`Total elapsed time: ${ Date.now() - startTime } ms`);
}

const promise = Cluster.launch({
  concurrency: Cluster.CONCURRENCY_CONTEXT,
  maxConcurrency: 2,
  puppeteerOptions: {
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
  },
});

promise.then(async (cluster) => {
  const url = 'https://www.example.com';
  const clip: BoundingBox = { width: 500, height: 320, y: 0, x: 0 };
  const xpath = '//h1[text()="Example Domain"]';
  const headers = {};

  for (let count = 0; count < 5; count++) {
    screenshot(cluster, {
      url: url,
      clip: clip,
      waitXpath: xpath,
      headers: headers,
    }).then();
  }

  await cluster.idle();

  // 关闭浏览器集群
  await cluster.close();
});
