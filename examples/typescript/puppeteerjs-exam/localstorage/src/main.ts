import { launch, Browser } from 'puppeteer';

async function setLocalstorage(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    const storageItems = {
      ['reqid']: '35741628143751486',
    };
    await page.evaluateOnNewDocument((items) => {
      const keys = Object.keys(items);
      for (const key of keys) {
        localStorage.setItem(key, items[key]);
      }
    }, storageItems);

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
  await setLocalstorage(browser, 'https://www.example.com');

  await browser.close();
});
