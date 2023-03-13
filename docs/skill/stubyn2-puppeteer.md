# 学习笔记2: Puppeteer

- [打开一个页面](#打开一个页面)
- [截图](#截图)
- [设置页面宽高](#设置页面宽高)
- [设置本地存储](#设置本地存储)
- [设置请求头](#设置请求头)
- [等待页面 XPath](#等待页面-xpath)

## 打开一个页面

调用 `launch` 函数启动一个浏览器，默认是 `Chrome`。`newPage` 函数打开一个新的标签页，`goto` 跳转到指定页面。

```typescript
import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
    defaultViewport: { width: 1920, height: 1080 },
  });

  const page = await browser.newPage();
  try {
    await page.goto('https://www.example.com', {
      waitUntil: 'domcontentloaded',
    });

    console.log('Went to:', page.url());
  } finally {
    await page.close();
  }

  await browser.close();
}

main();
```

## 截图

调用 `screenshot` 函数对当前页面截图。

```typescript
import { launch } from 'puppeteer';
import { writeFileSync } from 'fs';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
    defaultViewport: { width: 1920, height: 1080 },
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

main();
```

## 设置页面宽高

`setViewport` 函数接收一个对象，对象中 `width`、`height` 设置页面的宽高。

```typescript
import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
    defaultViewport: { width: 1920, height: 1080 },
  });

  const page = await browser.newPage();

  await page.setViewport({ width: 500, height: 500 });

  await page.goto('https://www.example.com', { waitUntil: 'domcontentloaded' });

  await page.screenshot({ path: 'dist/example.png', type: 'png' });
}

main();
```

## 设置本地存储

调用 `localStorage.setItem` 函数设置本地存储。

```typescript
import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
    defaultViewport: { width: 1920, height: 1080 },
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
```

## 设置请求头

`setExtraHTTPHeaders` 函数接收一个记录，将记录的键值添加到请求头中。

```typescript
import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
    defaultViewport: { width: 1920, height: 1080 },
  });

  const page = await browser.newPage();

  const extraHeaders = {
    ['reqid']: '35741628143751486',
  };
  page.setExtraHTTPHeaders(extraHeaders);

  await page.goto('https://www.example.com', { waitUntil: 'domcontentloaded' });
}

main();
```

## 等待页面 XPath

`waitForXPath` 函数接收一个 `XPath` 表达式，当指定的表达式出现在页面中后，才执行下一步代码，`timeout` 设置等待超时。

```typescript
import { launch } from 'puppeteer';

async function main() {
  const browser = await launch({
    headless: false,
    args: ['--no-sandbox', '--disable-setuid-sandbox'],
    defaultViewport: { width: 1920, height: 1080 },
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

main();
```
