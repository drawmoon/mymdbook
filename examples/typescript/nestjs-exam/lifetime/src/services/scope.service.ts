import { Injectable, Scope } from '@nestjs/common';
import { SingletonService } from './singleton.service';

@Injectable({
  // 将生命周期设置为 Request
  scope: Scope.REQUEST,
})
export class ScopeService {
  constructor(
    // 在 Request 生命周期的对象中注入 Default 生命周期的对象时，不会影响到当前对象的生命周期
    private readonly singletonService: SingletonService,
  ) {
    console.log('进入 ScopeService 构造函数');
  }
}
