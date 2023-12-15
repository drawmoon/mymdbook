/*
  方法装饰器
 */
function catchErr() {
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

class SomeClass {
  @catchErr()
  hello() {
    console.log('Hello World!');
    throw new Error('发送错误');
  }
}

const someClass = new SomeClass();
someClass.hello();
