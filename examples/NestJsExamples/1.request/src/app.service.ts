import { Inject, Injectable, Scope } from '@nestjs/common';
import { REQUEST } from '@nestjs/core';
import { Request } from 'supertest';
import { UserAccessor } from './user.accessor';

@Injectable(/*{ scope: Scope.DEFAULT }*/)
export class AppService {
  constructor(
    // 注入了 REQUEST 生命周期的对象后，该对象的生命周期会变更为 REQUEST
    // @Inject(REQUEST)
    // private readonly request: Request,
    // private readonly userAccessor: UserAccessor,
  ) {
    console.log('Invoke AppService constructor');
  }

  getHello(req: Request): string {
    console.log('Invoke getHello');

    return 'Hello World!';
  }
}
