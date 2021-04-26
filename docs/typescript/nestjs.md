# Nest Notes

- [处理 form-data 的请求](#处理-form-data-的请求)
- [执行文件下载操作](#接口执行文件下载操作)
- [创建系统级的身份标识](#创建系统级的身份标识)
- [允许未通过身份验证的用户访问路由](#允许未通过身份验证的用户访问路由)
- [在 NestJs 中使用多个环境变量文件（开发、生产）](#在-nestjs-中使用多个环境变量文件（开发、生产）)

## 处理 form-data 的请求

通过`npm`安装依赖项`npm install express-form-data`，或通过`yarn`安装依赖项`yarn add express-form-data`。

新建`interceptors/form-data.interceptor.ts`文件

```typescript
import {
  CallHandler,
  ExecutionContext,
  Injectable,
  NestInterceptor,
} from "@nestjs/common";
import { Observable, Subject } from "rxjs";
import * as os from "os";
import * as FormData from "express-form-data";

@Injectable()
export class FormDataInterceptor implements NestInterceptor {
  intercept(
    context: ExecutionContext,
    next: CallHandler<any>
  ): Observable<any> | Promise<Observable<any>> {
    const options = {
      uploadDir: os.tmpdir(),
      autoClean: true,
    };

    const http = context.switchToHttp();

    const subject = new Subject();
    FormData.parse(options)(http.getRequest(), http.getResponse(), () => {
      next.handle().subscribe(subject);
    });

    return subject;
  }
}
```

在方法上使用`@UseInterceptors(FormDataInterceptor)`拦截器，以处理`form-data`的请求。如果想在 Swagger 中测试接口，添加`@ApiConsumes('multipart/form-data')`装饰器描述内容编码类型

```typescript
export class FolderController {
  @ApiConsumes("multipart/form-data")
  @Put(":id/name")
  @UseInterceptors(FormDataInterceptor)
  updateName(
    @Param("id") id: number,
    @Body("newName") newName: string
  ): FolderDTO {}
}
```

## 执行文件下载操作

新建文件`shared/lib/http-utilities.ts`，新建`download`方法

```typescript
import { Response } from "express";
import { Readable } from "stream";

export function download(
  res: Response,
  buffer: Buffer,
  filename: string
): void {
  const stream = new Readable();
  stream.push(buffer);
  stream.push(null);

  res.set({
    "Content-Type": "application/octet-stream",
    "Content-Length": buffer.length,
    "Content-Disposition": `attachment; filename="${encodeURI(filename)}"`, // 指定下载的文件名称
  });

  stream.pipe(res);
}
```

在控制器中调用`download`方法，下载文件

```typescript
import { Get, Res } from "@nestjs/common";
import { Response } from "express";
import { download } from "../shared/lib/http-utilities";

export class FileController {
  @Get("docx")
  exportDocx(@Res() res: Response): void {
    const buffer = this.fileService.getDocxBuffer();
    download(res, buffer, "example.docx");
  }
}
```

## 创建系统级的身份标识

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

## 允许未通过身份验证的用户访问路由

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

## 在 NestJs 中使用多个环境变量文件（开发、生产）

用`Npm`安装依赖项`npm install --save dotenv`，或用`Yarn`安装`yarn add dotenv`

创建`.env`、`.env.development`文件，`.env`用于生产环境，`.env.development`用于开发环境。接下来修改`package.json`的`scripts`

```json
{
  "scripts": {
    "start": "node -r dotenv/config \"./node_modules/@nestjs/cli/bin/nest.js\" start dotenv_config_path=.env.development",
    "start:prod": "node -r dotenv/config dist/main.js dotenv_config_path=.env",
	"start:dev": "node --watch -r dotenv/config \"node_modules/@nestjs/cli/bin/nest.js\" start dotenv_config_path=.env.development",
	"start:debug": "node --debug -r dotenv/config \"node_modules/@nestjs/cli/bin/nest.js\" start dotenv_config_path=.env",
	"start:inspect": "node --inspect -r dotenv/config \"node_modules/@nestjs/cli/bin/nest.js\" start dotenv_config_path=.env"
  }
}
```
