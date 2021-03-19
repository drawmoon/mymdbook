# Puppeteer Notes

## 设置 localStorage

```typescript
const browser = await puppeteer.launch({ headless: true });

const page = await this.browser.newPage();

// Set local storage.
await page.evaluateOnNewDocument(() => {
  localStorage.setItem('item1', 'abc');
});

await page.goto('http://url...', { waitUntil: 'domcontentloaded' });
```

## 设置 Cookie

```typescript
const browser = await puppeteer.launch({ headless: true });

const page = await this.browser.newPage();

// Set Cookie.
await page.setExtraHTTPHeaders({ ['Cookie']: 'cookie...' });

await page.goto('http://url...', { waitUntil: 'domcontentloaded' });
```
