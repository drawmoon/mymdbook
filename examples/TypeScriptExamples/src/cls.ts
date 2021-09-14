// 方法重载
interface User {
  id: string;
}

export class UserService {
  getUser(id: string);
  getUser(options: (users: User[]) => User);
  getUser(id: string | ((users: User[]) => User), callback?: () => void) {
    console.log(id, callback);
  }
}

const userService = new UserService();
userService.getUser('1');
userService.getUser((us) => us[0]);

// 类继承
export class Foo {
  constructor(val: string) {
    console.log(val);
  }

  public print() {
    console.log('Foo hello');
  }
}

export class Bar extends Foo {
  constructor() {
    super('Bar ctor param');
  }

  public print() {
    console.log('Bar hello');
    super.print();
  }
}

const foo = new Foo('Foo ctor param');
foo.print();

const bar = new Bar();
bar.print();
