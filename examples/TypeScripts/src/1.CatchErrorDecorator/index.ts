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

class C {
  @errorCatch()
  fn(): void {
    console.log('test');
    throw new Error('error');
  }
}

const c = new C();
c.fn();
