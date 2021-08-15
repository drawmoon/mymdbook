import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
  });

  const page = await browser.newPage();

  await page.setViewport({ width: 500, height: 500 });

  await page.goto('https://www.example.com', { waitUntil: 'domcontentloaded' });

  await page.screenshot({ path: 'dist/example.png', type: 'png' });
}

main();
