import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
  });

  const page = await browser.newPage();
  try {
    await page.goto('https://www.example.com', {
      waitUntil: 'domcontentloaded',
    });

    console.log('Domcontent loaded.');

    await page.waitForXPath('//h1[text()="Example Domain"]', { timeout: 4000 });

    console.log('Wait for xpath.');

    // 匹配满足其中一个元素
    await page.waitForXPath(
      '//title[text() = "Example Domain"] | //h1[text()="Example Domain"]',
      { timeout: 4000 },
    );
  } finally {
    await page.close();
  }

  await browser.close();
}

main().then();
