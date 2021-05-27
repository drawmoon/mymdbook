# 在 NestJs 应用中创建系统级的身份标识

新建`user.accessor.ts`文件，用于存储当前用户信息，提供给`services`调用

```typescript
export interface IUserAccessor {
  current: UserDTO;
}
```

新建`user.service.ts`文件，实现`IUserAcessor`接口

```typescript
import { IUserAccessor } from "./user.accessor";
import { Injectable } from "@nestjs/common";

@Injectable()
export class UserService implements IUserAccessor {
  current: UserDTO;
}
```

新建`unify.guard.ts`文件，用于处理身份认证，并将当前用户信息赋值给`IUserAccessor`，在 API 被请求时，会先执行`canActivate`中的代码

```typescript
import {
  CanActivate,
  ExecutionContext,
  Inject,
  Injectable,
} from "@nestjs/common";
import { Observable } from "rxjs";
import { IUserAccessor, USER_ACCESSOR } from "../shared/services/user.accessor";

@Injectable()
export class UnifyGuard implements CanActivate {
  constructor(
    @Inject(USER_ACCESSOR)
    private readonly userAccessor: IUserAccessor
  ) {}

  canActivate(
    context: ExecutionContext
  ): boolean | Promise<boolean> | Observable<boolean> {
    this.userAccessor.current = {
      id: 1,
      username: "ioea-admin",
      email: "ioea-admin@ioea.com",
      password: "123456",
      roles: ["admin"],
      claims: [],
    };
    return true;
  }
}
```

注册到容器中

```typescript
const userAccessorProvider = {
  provide: "USER_ACCESSOR",
  useClass: UserService,
};

@Module({
  providers: [
    userAccessorProvider,
    {
      provide: APP_GUARD,
      useClass: UnifyGuard,
    },
  ],
  exports: [userAccessorProvider],
})
export class AuthModule {}
```

在`services`中调用

```typescript
export class FileService {
  constructor(
    @Inject("USER_ACCESSOR")
    private readonly userAccessor: IUserAccessor
  ) {}

  add(file: FileDTO) {
    file.owner = this.userAccessor.current.id;
  }
}
```
