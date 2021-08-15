import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
  });

  const page = await browser.newPage();

  const storageItems = {
    ['reqid']: '35741628143751486',
  };
  page.evaluateOnNewDocument((items) => {
    const keys = Object.keys(items);
    for (const key of keys) {
      localStorage.setItem(key, items[key]);
    }
  }, storageItems);

  await page.goto('https://www.example.com', { waitUntil: 'domcontentloaded' });
}

main();
