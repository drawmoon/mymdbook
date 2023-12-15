# Puppeteer

- [打开一个页面](#打开一个页面)
- [页面截图](#页面截图)
- [设置页面的宽高](#设置页面的宽高)
- [设置浏览器的本地存储](#设置浏览器的本地存储)
- [为每个请求设置请求头](#为每个请求设置请求头)
- [等待页面 XPath](#等待页面-xpath)
- [打印页面为 PDF](#打印页面为-pdf)

## 打开一个页面

调用 `launch` 函数启动一个浏览器，浏览器默认是 `Chrome`；`newPage` 函数打开一个新的标签页；`goto` 跳转到指定页面，配置 `waitUntil: 'domcontentloaded'` 时，表示等待页面加载完成。

```typescript
import { launch, Browser } from 'puppeteer';

async function openPage(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    await page.goto(url, { waitUntil: 'domcontentloaded' });
    
    console.log('Went to:', page.url());
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
  await openPage(browser, 'https://www.example.com');

  await browser.close();
});
```

## 页面截图

调用 `screenshot` 函数对当前页面截图。你可以加上 `path` 参数，指定文件保存的位置，当 `path` 为空时，它将返回一个 `Buffer`；它还接收 `encoding` 参数，指定返回的数据是 `Buffer` 或 `Base64` 字符串。

```typescript
import { launch, Browser } from 'puppeteer';

async function screenshot(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    await page.goto(url, { waitUntil: 'domcontentloaded' });
  
    // 保存文件到指定位置
    await page.screenshot({ path: 'example.png' });

    // 指定 encoding 返回二进制数据或 base64 字符串
    const buf = await page.screenshot({ encoding: 'binary' });
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
  await screenshot(browser, 'https://www.example.com');
  
  await browser.close();
});
```

## 设置页面的宽高

`setViewport` 函数接收一个对象，对象中 `width`、`height` 设置页面的宽高。

```typescript
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
```

## 设置浏览器的本地存储

调用 `localStorage.setItem` 函数设置本地存储。

```typescript
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
```

## 为每个请求设置请求头

`setExtraHTTPHeaders` 函数接收一个 `Record<string, string>` 对象，将 `Record` 的键值添加到请求头中。

```typescript
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
```

## 等待页面 XPath

`waitForXPath` 函数接收一个 `XPath` 表达式，当指定的表达式出现在页面中后，才执行下一步代码，`timeout` 设置等待超时。

```typescript
import { launch, Browser } from 'puppeteer';

async function waitXpath(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    await page.goto('https://www.example.com');

    await page.waitForXPath('//h1[text()="Example Domain"]', { timeout: 4000 });
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
```

## 打印页面为 PDF

调用 `pdf` 函数将当前页面打印为 PDF。你可以加上 `path` 参数，指定文件保存的位置，当指定 `path` 的情况下，默认返回二进制数据；你还可以通过 `format` 参数指定 PDF 的纸张尺寸为 `a4`。

```typescript
import { launch, Browser } from 'puppeteer';

async function toPdf(browser: Browser, url: string) {
  const page = await browser.newPage();
  try {
    await page.goto(url, { waitUntil: 'domcontentloaded' });

    // 保存文件到指定位置
    await page.pdf({ path: 'example.pdf', format: 'a4' });

    // 不指定 path 的情况下，默认返回二进制数据
    const buf = await page.pdf({ format: 'a4' });
  } finally {
    await page.close();
  }
}

const promise = launch({
  headless: true, // 在使用 `pdf` 函数时，需要将 `headless` 设置为 `true`，否则它将无法工作，你将得到 `Protocol error (Page.printToPDF): PrintToPDF is not implemented` 的报错
  args: ['--no-sandbox', '--disable-setuid-sandbox'],
  defaultViewport: { width: 1920, height: 1080 },
});

promise.then(async (browser) => {
  await toPdf(browser, 'https://www.example.com');

  await browser.close();
});
```

> 在使用 `pdf` 函数时，你需要将 `headless` 设置为 `true`，否则它将无法工作，你将得到 `Protocol error (Page.printToPDF): PrintToPDF is not implemented` 的报错。
