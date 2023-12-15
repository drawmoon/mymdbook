import {Browser, launch} from 'puppeteer';

async function setHeaders(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    const extraHeaders = {
      ['reqid']: '35741628143751486',
    };
    await page.setExtraHTTPHeaders(extraHeaders);

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
  await setHeaders(browser, 'https://www.example.com');

  await browser.close();
});
