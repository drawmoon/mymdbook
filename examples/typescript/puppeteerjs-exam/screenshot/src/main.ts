import { launch, Browser } from 'puppeteer';

async function screenshot(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    await page.goto(url, { waitUntil: 'domcontentloaded' });

    // 保存文件到指定位置
    await page.screenshot({ path: 'example.png' });

    // 指定 encoding 返回二进制数据或 base64 字符串
    // const buf = await page.screenshot({ encoding: 'binary' });
  } finally {
    await page.close();
  }
}

const promise = launch({
  headless: false,
  args: ['--no-sandbox', '--disable-setuid-sandbox'],
  defaultViewport: { width: 1920, height: 1080 },
});

promise.then(async (browser) => {
  await screenshot(browser, 'https://www.example.com');

  await browser.close();
});
