import { launch, Browser } from 'puppeteer';

async function setViewport(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    await page.setViewport({ width: 500, height: 500 });

    await page.goto(url, { waitUntil: 'domcontentloaded' });
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
  await setViewport(browser, 'https://www.example.com');

  await browser.close();
});
