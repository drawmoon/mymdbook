function errorCatch() {
  return function (
    target: any,
    propertyKey: string,
    descriptor: PropertyDescriptor,
  ): void {
    const original = descriptor.value;
    descriptor.value = function () {
      try {
        original.apply(this);
      } catch (e) {
        console.log(e);
      }
    };
  };
}

class Foobar {
  @errorCatch()
  foo(): void {
    console.log('foo');
    throw new Error('error');
  }
}

const c = new Foobar();
c.foo();
