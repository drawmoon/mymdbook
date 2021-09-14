import { Injectable, Scope } from '@nestjs/common';
import { SingletonService } from './singleton.service';

@Injectable({
  scope: Scope.REQUEST,
})
export class ScopeService {
  constructor(
    // 在生命周期为 REQUEST 的对象中注入生命周期为 DEFAULT 的对象时，不会影响到注入的对象
    private readonly singletonService: SingletonService,
  ) {
    console.log('进入 ScopeService 构造函数');
  }
}
