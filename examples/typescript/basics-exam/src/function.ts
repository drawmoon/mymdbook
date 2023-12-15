/*
  函数声明
 */
function someFn(): void {

}

/*
  函数的参数声明
 */
function someFn2(args: string[], force: boolean): void {

}

/*
  剩余参数
 */
function someFn3(...args: string[]): void {

}

someFn3('a1', 'a2', 'a3');

/*
  匿名函数
 */
const someFn4 = function () {

};

/*
  箭头函数
 */
const someFn5 = () => {

};

// 箭头函数中的泛型参数
const someFn6 = <T>(id: T) => {

};

/*
  回调函数
 */
function someFn7(callback: () => void): void {
  callback();
}

function callbackFn() {
  console.log('Hello World!');
}
someFn7(callbackFn);

// 匿名回调函数
someFn7(function () {
  console.log('Hello World!');
});

// 箭头回调函数
someFn7(() => {
  console.log('Hello World!');
});

/*
  异步的回调函数
 */
async function someFn8(callback: () => Promise<void>): Promise<void> {
  await callback();
}

someFn8(async () => {
  console.log('Hello World!');
});

/*
  回调函数中的泛型参数
 */
function someFn9(callback: <T>(id: T) => void): void {
  callback(123);
}

someFn9((id) => {
  console.log(`Id is ${id}!`);
});

/*
  闭包
 */
function someFn10() {
  return function (msg: string) {
    console.log(msg);
  };
}

const msg = 'Hello World!';
someFn10()(msg);
