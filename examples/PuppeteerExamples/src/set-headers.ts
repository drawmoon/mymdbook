import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
  });

  const page = await browser.newPage();

  const extraHeaders = {
    ['reqid']: '35741628143751486',
  };
  page.setExtraHTTPHeaders(extraHeaders);

  await page.goto('https://www.example.com', { waitUntil: 'domcontentloaded' });
}

main().then();
