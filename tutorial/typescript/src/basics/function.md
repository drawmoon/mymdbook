# 函数

## 函数声明

```typescript
function sayHi(): void {
  console.log('Hello, World!');
}

// 函数的入参、出参
function sum(x: number, y: number): number {
  return x + y;
}
```

## 剩余参数

```typescript
function sum(...args: number[]): number {
  let i = 0;
  for (const item of args) {
    i = i + item;
  }
  return i;
}

const result = sum(1, 10, 25);
```

## 函数表达式

```typescript
const sayHi = function () {
  console.log('Hello, World!');
};

// 箭头函数
const sayHi = () => {
  console.log('Hello, World!');
};

// 箭头函数的泛型参数
const find = <T>(id: T) => {
  console.log(id);
};
```

## 回调函数

```typescript
function read(callback: () => void): void {
  callback();
}

// 回调函数中的泛型参数
function read(callback: <T>(id: T) => void): void {
  callback(123);
}

// 异步的回调函数
async function read(callback: () => Promise<void>): Promise<void> {
  await callback();
}
```

## 闭包

```typescript
function sum(x: number, y: number) {
  const result = x + y;
  return function (msg: string) {
    console.log(msg, result);
  };
}

sayHi(1, 2)('result:');
```