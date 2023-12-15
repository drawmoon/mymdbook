import { launch, Browser } from 'puppeteer';

async function toPdf(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    await page.goto(url, { waitUntil: 'domcontentloaded' });

    // 保存文件到指定位置
    await page.pdf({ path: 'example.pdf', format: 'a4' });

    // 不指定 path 时返回二进制数据
    // const buf = await page.pdf({ format: 'a4' });
  } finally {
    await page.close();
  }
}

const promise = launch({
  headless: true, // 在使用 `pdf` 函数时，需要将 `headless` 设置为 `true`，否则它将无法工作，你将得到 `Protocol error (Page.printToPDF): PrintToPDF is not implemented` 的报错
  args: ['--no-sandbox', '--disable-setuid-sandbox'],
  defaultViewport: { width: 1920, height: 1080 },
});

promise.then(async (browser) => {
  await toPdf(browser, 'https://www.example.com');

  await browser.close();
});
