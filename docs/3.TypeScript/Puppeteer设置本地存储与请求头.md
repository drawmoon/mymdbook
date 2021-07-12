# Puppeteer 设置本地存储与请求头

## 设置本地存储

```typescript
const browser = await puppeteer.launch({ headless: true });

const page = await this.browser.newPage();

// Set local storage.
await page.evaluateOnNewDocument(() => {
  localStorage.setItem('item1', 'abc');
});

await page.goto('http://example.com', { waitUntil: 'domcontentloaded' });
```

携带参数：

```typescript
const localStorageItems: Record<string, string> = {
  ['item1']: 'abc',
  ['item2']: 'def',
};

const browser = await puppeteer.launch({ headless: true });

const page = await this.browser.newPage();

// Set local storage.
await page.evaluateOnNewDocument((keyValues) => {
  const keys = Object.keys(keyValues);
  for (const key of keys) {
    localStorage.setItem(key, keyValues[key]);
  }
}, localStorageItems);

await page.goto('http://example.com', { waitUntil: 'domcontentloaded' });
```

## 设置请求头

```typescript
const browser = await puppeteer.launch({ headless: true });

const page = await this.browser.newPage();

// Set Cookie.
await page.setExtraHTTPHeaders({ ['Cookie']: 'cookie...' });

await page.goto('http://example.com', { waitUntil: 'domcontentloaded' });
```
