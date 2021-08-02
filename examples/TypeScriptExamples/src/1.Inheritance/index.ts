export class Foo {
  constructor(val: string) {
    console.log(val);
  }

  public hello() {
    console.log('Foo hello');
  }
}

export class Bar extends Foo {
  constructor() {
    super('Bar ctor param');
  }

  public hello() {
    console.log('Bar hello');
    super.hello();
  }
}

const foo = new Foo('Foo ctor param');
foo.hello();

const bar = new Bar();
bar.hello();
