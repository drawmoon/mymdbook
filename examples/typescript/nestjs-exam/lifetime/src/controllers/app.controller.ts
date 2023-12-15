import { Controller, Get, Scope } from '@nestjs/common';
import { ScopeService } from '../services/scope.service';
import { TransientService } from '../services/transient.service';

// 在 NestJs 的服务容器中，对象被构建时，有三种生命周期，分别是：
// 1. Default: 在整个应用中只会被构建一次，在应用启动时被构建
// 2. Request: 在每一个请求中被构建，实例将在请求完成处理后被销毁
// 3. Transient: 从容器中请求时被构建，每次都是一个新的实例
// 在 NestJs 中，所有实例的生命周期默认都是 Default 的，即单例
@Controller({
  // scope: Scope.REQUEST,
})
export class AppController {
  constructor(
    // 如果在 Default 生命周期的对象中注入 Request 生命周期的对象时，前者的生命周期也会变更为 Request
    // private readonly scopeService: ScopeService,
    // 如果在 Default 生命周期的对象中注入 Transient 生命周期的对象时，Transient 生命周期的对象只会被实例化一次
    private readonly transientService: TransientService,
  ) {
    console.log('进入 AppController 构造函数');
  }

  @Get()
  get(): string {
    return 'Hello, World!';
  }
}
