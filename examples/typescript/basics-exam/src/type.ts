/*
  基础类型判断
 */
const n = 1;
const s = 'abc';

console.log(typeof n === 'number'); // true
console.log(typeof s === 'string'); // true

/*
  对象类型判断
 */
class Foo {
  fly() {
    console.log('fly');
  }
}

class Bar {
  run() {
    console.log('run');
  }
}

const foo = new Foo();
const bar = new Bar();

console.log(typeof foo, typeof bar); // object, object
console.log(foo instanceof Foo, bar instanceof Bar); // true, true

function isFoo(o: Foo | Bar) {
  console.log(o instanceof Foo);
}

isFoo(foo); // true

/*
  接口类型判断
 */
interface Foobar {
  foo: Foo;
  bar: Bar;
}

const foobar: Foobar = {
  foo: { fly: () => console.log('fly') },
  bar: { run: () => console.log('run') },
};

isFoo(foobar.foo); // false

function isBar(o: Bar) {
  console.log('run' in o);
}

isBar(foobar.bar); // true

/*
  判断是否为函数
 */
type Task = () => void;

function isFunction(data: string | Task): data is Task {
  return typeof data === 'function';
}

function handle(data: string | Task) {
  if (isFunction(data)) {
    (data as Task)();
  } else {
    console.log(data);
  }
}

handle('Hello');
handle(() => console.log('Hi'));
