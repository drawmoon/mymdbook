import { Inject, Injectable, Scope } from '@nestjs/common';
import { REQUEST } from '@nestjs/core';
import { Request } from 'supertest';
import { UserAccessor } from './user.accessor';

@Injectable(/*{ scope: Scope.DEFAULT }*/)
export class AppService {
  constructor(
    // 注入了生命周期为 REQUEST 的对象后，当前对象的生命周期会变更为 REQUEST
    // @Inject(REQUEST)
    // private readonly request: Request,
    private readonly userAccessor: UserAccessor,
  ) {
    console.log('进入 AppService 构造函数');
  }

  getHello(req: Request): string {
    return `Hello ${this.userAccessor.current}!`;
  }
}
