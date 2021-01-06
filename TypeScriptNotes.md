# TypeScript

## Install & Cmd

`npm install -g typescript` & `tsc`

## 数据类型

- boolean

```ts
var enable: boolean = false;
```

- number

```ts
var index: number = 1;
```

- string

```ts
var text: string = "Holle World";
```

- array

```ts
var numbers: number[] = [1, 2];
var texts: string[] = ["A", "B"];

// Array<T>

var arr: Array<number> = [1, 2];
```

- tuple

```ts
var tuple: [number, string] = [1, "A"];
```

- enum

```ts
enum Status {
  error = -1,
  success = 1,
}

var status: Status = Status.success;
```

- any

```ts
var a: any = 1;
a = true;

// any array

var arr: any[] = [1, "A", true];

// common usage

var box: any = document.getElementById("box");
```

- null

```ts
var n: null = null;
```

- undefined

```ts
var u: undefined = undefined;
```

- void

```ts
function main(): void {
  console.log("Holle World");
}
```

- never

```ts
var a: never;
a = (() => {
  throw new Error("error message");
})();
```

## 函数

- 函数的声明

```ts
function main(): void {}
```

- 可选参数

```ts
function get(id: number, search?: string): string {}
```

- 默认参数

```ts
function get(id: number, include: string[] | null = null): string {}
```

- 剩余参数

```ts
function get(id: number, ...include: string[]): string {}
```

- 重载

```ts
function get(id: number): string;
function get(name: string): string;
function get(id: number, include: boolean): string;

function get(param: any, include?: boolean): string {}
```

- 箭头函数

```ts
setTimeout(() => {});
```

## 类

- 定义类

```ts
class Animal {}
```

- 属性

```ts
class Animal {
  name: string;
}
```

- 方法

```ts
class Animal {
  getName(): string {
    return "dog";
  }
}
```

- 构造函数

```ts
class Animal {
  constructor() {}
}

var animal = new Animal();

// or

class Animal {
  name: string;
  constructor(name: string) {
    this.name = name;
  }
}

var animal = new Animal({
  name: "dubu",
});
```

- 继承

```ts
class Animal {}

class Dog extends Animal {}

// 初始化基类构造参数

class Animal {
  name: string;

  constructor(name: string) {
    this.name = name;
  }
}

class Dog extends Animal {
  constructor() {
    super("dubu");
  }
}
```

- 重写

```ts
class Animal {
  name: string;

  constructor(name: string) {
    this.name = name;
  }

  getName(): string {
    return this.name;
  }
}

class Dog extends Animal {
  constructor() {
    super("dubu");
  }

  getName(): string {
    return `name is ${this.name}`;
  }
}
```

- 修饰符
  - public
  - protected
  - private
  - 不添加修饰符，默认为 `public`

```ts
class Tea {
  private type: string;
}
```

- 静态属性

```ts
class Tea {
  static type: string;
}
```

- 静态方法

```ts
class Ext {
  static getStr(): string {
    return "string";
  }
}
```

- 抽象类

```ts
abstract class Animal {
  abstract getName(): string;
}

class Dog extends Animal {
  getName(): string {
    rteurn "dubu";
  }
}
```

## 接口

- 属性类型接口

```ts
interface Result {
  succeed: boolean;
  message?: string;
  data?: string;
}

function showResult(result: Result): string {}
```

- 函数类型接口

```ts
interface Encrypt {
  (value: string, key: string, iv?: string): string;
}

var md5: Encrypt = function (
  value: string,
  key: string,
  iv?: string
): string {};
var sha1: Encrypt = function (
  value: string,
  key: string,
  iv?: string
): string {};
```

- 可索引接口

```ts
// 对数组的约束
interface KeyValue {
  [index: number]: string;
}

var arr: KeyValue = ["a", "b"];
arr[0];

// 对对象的约束
interface User {
  [index: string]: string;
}

var user: User = { name: "xiaoli" };
```

- 类类型接口

```ts
interface Animal {
  name: string;
  eat(food: string): void;
}

class Dog implements Animal {
  name: string;
  eat(food: string) {
    console.log(`eat ${food}`);
  }
}
```

- 接口继承

```ts
interface HasCreated {
  created: any;
}

interface Log extends HasCreated {
  type: number;
  created: any;
  debug(message: string): void;
}

class LogImpl implements Log {
  type: number;
  created: any;
  debug(message: string): void {
    console.log(message);
  }
}
```

## 泛型

- 泛型函数

```ts
function showValue<T>(val: T): void {
  console.log(val);
}

showValue<number>(123);
```

- 泛型类

```ts
class List<T> {
  arr: T[] = [];
  add(val: T): void {
    arr.push(val);
  }
}

var nums = new List<number>();
nums.add(1);

var strs = new List<string>();
strs.add("a");
```

- 泛型接口

```ts
// 泛型函数的接口

interface ShowValueFn<T> {
  <T>(val: T): void;
}

var showValue: ShowValueFn = function <T>(val: T): void {
  console.log(val);
};

showValue<string>("abc");

// or

interface ShowValueFn<T> {
  (val: T): void;
}

var showValue: ShowValueFn<string> = function <T>(val: T): void {
  console.log(val);
};

showValue("abc");

// 泛型类的接口

interface List<T> {
  add(val: T): void;
}

class ListImpl<T> implements List<T> {
  arr: T[] = [];
  add(val: T): void {
    arr.push(val);
  }
}

var list = new ListImpl<number>();
list.add(1);
```

## 模块

```ts
// modules/user.ts
export function get(): vod {
  console.log("get");
}

// index.ts
import { get } from "./modules/user";

get();

// or

// modules/user.ts
export default function get(): vod {
  console.log("get");
}

// index.ts
import get from "./modules/user";

get();
```

## 命名空间

```ts
namespace Model {
  export class User {
    name: string;
    age: number;
  }
}

var user = new Model.User();
```

## 装饰器

装饰器是一种特殊的类型声明，它能够被附加到类的声明、方法、属性或参数上，可以修改类的行为。

装饰器的执行顺序：
属性装饰器->方法装饰器->参数装饰器->类装饰器；如果存在多个同类型的装饰器，会优先执行后面的装饰器。

- 类装饰器

```ts
// 类装饰器表达式会在运行时当作函数被调用，类的构造函数作为其唯一的参数。
// 如果类装饰器返回一个值，它会使用提供的构造函数来替换类的声明。

// 普通装饰器
function HttpClientExt(params: any) {
  params.prototype.baseUrl: string;
  params.prototype.send = function(): void {
    console.log("send");
  }
}

@HttpClientExt
class HttpClient {
  constructor() {}
  get(): void {
    console.log("get");
  }
}

var client = new HttpClient();
client.baseUrl = "http://localhost";
client.get();
client.send();

// 装饰器工厂
function HttpClientExt(params: any) {
  return function(target: any) {
    target.prototype.baseUrl = params;
    target.prototype.send = function(): void {
      console.log("send");
    }
  }
}

@HttpClientExt("http://lcoalhost")
class HttpClinet {
  constructor() {}
  get(): void {
    console.log("get");
  }
}

// 重写构造函数
function HttpClientExt(params: any) {
  return class extends target {
    console.log("constructor2");

    get(): void {
      console.log("get");
    }
  }
}

@HttpClientExt
class HttpClient {
  constructor() {
    console.log("constructor");
  }
  get(): void {
    console.log("get");
  }
}

var client = new HttpClient();
client.baseUrl = "http://localhost";
client.get();
client.send();
```

- 属性装饰器

```ts
// 属性装饰器有两个参数
// 1. 如果是静态属性，参数为类的构造函数，否则参数为类的原型对象。
// 2. 属性的名称。

function PropertyExt(params: any) {
  return function (target: any, attr: any) {
    target[attr] = params;
  };
}

class HttpClient {
  @PropertyExt("http://localhost")
  baseUrl: string | undefined;
  constructor() {}
  get(): void {
    console.log("get");
  }
}

var client = new HttpClient();
console.log(client.baseUrl);
```

- 方法装饰器

```ts
// 方法装饰器应用到方法的属性描述符上，可以用来监视、修改或替换方法的定义。
// 方法装饰器有三个参数
// 1. 如果是静态方法，参数为类的构造函数，否则参数为类的原型对象。
// 2. 方法的名称。
// 3. 方法的属性描述符。

function MethodExt(params: any) {
  return function (target: any, methodName: any, desc: any) {
    var method = desc.value;
    desc.value = function (...args: any[]) {
      args.push("a");
      method.apply(this, args);
    };
  };
}

class HttpClient {
  constructor() {}
  @MethodExt
  get(...args: any[]): void {
    console.log(args);
  }
}
```

- 参数装饰器

```ts
// 参数装饰器会在运行时当作函数被调用，可以使用参数装饰器为类的原型增加一些元素。
// 参数装饰器有三个参数
// 1. 如果是静态方法，参数为类的构造函数，否则参数为类的原型对象。
// 2. 参数的名称。
// 3. 参数的索引。

function ParameterExt(params: any) {
  return function(target: any, paramName: any, paramIndex: any) {
    if (target[paramIndex] == undefined)
      console.log("undefined");
  }
}

class HttpClient {
  constructor() {}
  get(@ParameterExt url: string | undefined): void {
    console.log("get"));
  }
}
```
