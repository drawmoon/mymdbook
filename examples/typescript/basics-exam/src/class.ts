/*
  类的声明
 */
class Store {
  // 构造函数
  constructor(
    // 私有字段
    private name: string
  ) {}

  // 公共函数
  public getName(): string {
    return this.name;
  }

  // 受保护的函数
  protected getStyle(): string {
    return '';
  }

  // 私有函数
  private getOwner(): string {
    return '';
  }
}

// 构造实例
const store = new Store('Game World');

/*
  类的继承
 */
class PhysicalStore extends Store {
  // 构造函数重写
  constructor(name: string, public description: string) {
    super(name);
  }

  // 函数重写
  getName(): string {
    console.log('PhysicalStore getName');

    // 调用基类的函数
    return super.getName();
  }
}

/*
  函数重载
 */
class UserAccessor {
  public getPhone(id: number): string;

  public getPhone(name: string): string;

  public getPhone(option: number | string): string {
    return '';
  }
}

const userAccessor = new UserAccessor();
userAccessor.getPhone(1);
userAccessor.getPhone('George');

/*
  抽象类
 */
abstract class People {
  // 抽象字段
  public abstract name: string;

  // 抽象函数
  public abstract jog(): void;
}

/*
  静态成员
 */
class Starter {
  // 静态字段
  public static drive: string;

  // 静态函数
  public static startup(): void {

  }
}

/*
  接口实现
 */
interface IBackgroundTask {

}

class RefreshBackgroundTask implements IBackgroundTask {

}
