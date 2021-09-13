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

class TestCatchError {
  @errorCatch()
  fn(): void {
    console.log('fn');
    throw new Error('error');
  }
}

const c = new TestCatchError();
c.fn();
