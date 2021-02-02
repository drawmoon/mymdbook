function errorCatch(): Function {
  return function (
    target: any,
    propertyKey: string,
    descriptor: PropertyDescriptor
  ): void {
    var original = descriptor.value;
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
    console.log("test");
    throw new Error("error");
  }
}

var c = new C();
c.fn();
