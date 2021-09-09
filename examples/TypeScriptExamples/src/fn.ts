// 重载
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

// 继承
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
