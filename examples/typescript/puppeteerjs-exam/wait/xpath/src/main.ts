import { launch, Browser } from 'puppeteer';

async function waitXpath(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    await page.goto('https://www.example.com');

    await page.waitForXPath('//h1[text()="Example Domain"]', { timeout: 4000 });

    // 匹配满足其中一个元素
    // await page.waitForXPath(
    //   '//title[text() = "Example Domain"] | //h1[text()="Example Domain"]',
    //   { timeout: 4000 },
    // );
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
  await waitXpath(browser, 'https://www.example.com');

  await browser.close();
});
