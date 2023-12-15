# 异步

## Callback 异步模型

```typescript
function readFile(path: string, callback :(err: Error, data: Buffer) => void): void {
  setTimeout(() => {
    const data = Buffer.from('123456abcdefg', 'utf-8');
    callback(undefined, data);
  }, 2000);
}

readFile('text.txt', (err, data) => {
  if (err) {
    console.error(err.message);
  } else {
    console.log(data.toString('utf-8'));
  }
});
```

## Promise 异步模型

```typescript
function readFile(path: string): Promise<Buffer> {
  return new Promise<Buffer>((resolve, reject) => {
    setTimeout(() => {
      const data = Buffer.from('123456abcdefg', 'utf-8');
      resolve(data);
    }, 2000);
  });
}

readFile('text.txt')
  .then((data) => {
    console.log(data.toString('utf-8'));
  })
  .catch((err) => {
    console.error(err.message);
  });
```