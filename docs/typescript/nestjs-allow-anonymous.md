# NestJs 允许匿名用户访问接口

```ts
// guards.decorator.ts
import { SetMetadata } from '@nestjs/common';

export const ALLOW_ANONYMOUS_KEY = 'allowAnonymous';
export const AllowAnonymous = () => SetMetadata(ALLOW_ANONYMOUS_KEY, true);

// default.guard.ts
import { Injectable, CanActivate, ExecutionContext } from '@nestjs/common';
import { Reflector } from '@nestjs/core';

@Injectable()
export class RolesGuard implements CanActivate {
  constructor(private reflector: Reflector) {}

  canActivate(context: ExecutionContext): boolean {
    const allowAnonymous = this.reflector.get<boolean>(ALLOW_ANONYMOUS_KEY, context.getHandler());
    if (allowAnonymous) {
      return true;
    }

    // ...
  }
}

// license.controller.ts
import { AllowAnonymous } from './guards.decorator';

@AllowAnonymous()
@Get()
get(): string {
  // ...
}
```
