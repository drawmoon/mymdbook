import { Injectable, Scope } from '@nestjs/common';

export abstract class UserAccessor {
  abstract current?: string;
}

@Injectable({
  // scope: Scope.REQUEST,
  // scope: Scope.TRANSIENT, // 如果在生命周期为 DEFAULT 的对象中注入该对象，该对象只会实例化一次
})
export class UserService extends UserAccessor {
  private _currentUser?: string;

  get current(): string | undefined {
    return this._currentUser;
  }

  set(user?: string) {
    this._currentUser = user;
  }
}
