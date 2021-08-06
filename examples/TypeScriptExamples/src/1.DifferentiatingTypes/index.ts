const n = 1;
const s = 'abc';

console.log(typeof n === 'number'); // true
console.log(typeof s === 'string'); // true

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

const f = new Foo();
const b = new Bar();

console.log(typeof f, typeof b); // object, object
console.log(f instanceof Foo, b instanceof Bar); // true, true

function isFoo(o: Foo | Bar) {
  console.log(o instanceof Foo);
  console.log('fly' in o);
}

isFoo(f); // true, true

interface Foobar {
  foo: Foo;
  bar: Bar;
}

const foobar: Foobar = {
  foo: { fly: () => console.log('fly') },
  bar: { run: () => console.log('run') },
};

isFoo(foobar.foo); // false, true

type Task = () => void;

function isTaskFunction(data: string | Task): data is Task {
  return typeof data === 'function';
}

function handle(data: string | Task) {
  if (isTaskFunction(data)) {
    (data as Task)();
  } else {
    console.log(data);
  }
}

handle('Hello');
handle(() => console.log('Hi'));
