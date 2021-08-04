import { Injectable, Scope } from '@nestjs/common';

@Injectable({
  // scope: Scope.REQUEST,
  // scope: Scope.TRANSIENT, // 如果在 DEFAULT 生命周期的对象中注入该对象，该对象只会实例化一次
})
export class UserAccessor {}
