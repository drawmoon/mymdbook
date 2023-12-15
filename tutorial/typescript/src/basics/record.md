# 记录

## 扩展阅读

### 匿名函数消除过多 if-else 的方法

`if-else` 的写法：

```typescript
function sayHi(name: string, type: 'zh' | 'en' | 'ja'): string {
  if (type === 'zh') {
    return `你好, ${name}`;
  } else if (type === 'en') {
    return `hi, ${name}`;
  } else if (type === 'ja') {
    return `こんにちは, ${name}`;
  } else {
    // do something...
  }
}
```

匿名函数的写法：

```typescript
const record: Record<string, (name: string) => void> = {
  ['zh']: (name) => `你好, ${name}`,
  ['en']: (name) => `hi, ${name}`,
  ['ja']: (name) => `こんにちは, ${name}`,
};

function sayHi(name: string, type: 'zh' | 'en' | 'ja'): string {
  return record[type](name);
}
```