import { launch } from 'puppeteer';
import { writeFileSync } from 'fs';

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

    // 截图并保存到文件
    await page.screenshot({ path: 'dist/example.png', type: 'png' });

    // 截图并返回二进制数据
    const binary = await page.screenshot({ encoding: 'binary', type: 'png' });
    writeFileSync('dist/example1.png', binary as Buffer);
  } finally {
    await page.close();
  }

  await browser.close();
}

main().then();
