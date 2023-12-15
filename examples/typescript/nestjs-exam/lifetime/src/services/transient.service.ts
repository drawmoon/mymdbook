import { Injectable, Scope } from '@nestjs/common';
import { SingletonService } from './singleton.service';

@Injectable({
  // 将生命周期设置为 Transient
  scope: Scope.TRANSIENT,
})
export class TransientService {
  constructor(
    // 在 Transient 生命周期的对象中注入 Default 生命周期的对象时，不会影响到当前对象的生命周期
    private readonly singletonService: SingletonService,
  ) {
    console.log('进入 TransientService 构造函数');
  }
}
