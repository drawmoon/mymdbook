import { CanActivate, ExecutionContext, Injectable } from '@nestjs/common';
import { Observable } from 'rxjs';
import { UserAccessor, UserService } from './user.accessor';

const TEST_USERS = ['admin', 'zzs', 'yxl', 'yys'];

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private readonly userAccessor: UserAccessor) {}

  canActivate(
    context: ExecutionContext,
  ): boolean | Promise<boolean> | Observable<boolean> {
    (this.userAccessor as UserService).set(TEST_USERS.pop() as string);
    return true;
  }
}
