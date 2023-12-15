# Nest.js

- 基础
    - [依赖关系注入](#依赖关系注入)
    - [获取 Request、Response](#获取-requestresponse)
- Web Api 应用
    - [Swagger/OpenApi](#swaggeropenapi)
    - [处理 form-data 请求](#处理-form-data-请求)
- 高级
    - [下载二进制数据到客户端](#下载二进制数据到客户端)

## 基础

### 依赖关系注入

使用服务容器，注册服务时，可以设置服务在程序运行时的生存期，NestJs 支持以下三种服务生存期：

- `Default`: 在整个应用中只会被实例化一次，在应用程序启动时，会实例化所有 `Default` 生存期的服务。
- `Request`: 在每一个请求中被实例化，实例将在请求完成处理后被销毁。
- `Transient`: 从容器中请求时被实例化，每次都是一个新的实例。

在 NestJs 中，所有实例的生命周期默认都是 `Default`。

需要注意的是：

- 如果在 `Default` 生存期的服务中注入 `Request` 生存期的服务时，前者的生存期也会变更为 `Request`。
- 如果在 `Default` 生存期的服务中注入 `Transient` 生存期的服务时，`Transient` 生存期的服务只会被实例化一次。

示例中通过 `Injectable` 装饰器来指定服务的生存期：

```typescript
import { Injectable, Scope } from '@nestjs/common';

@Injectable({
  scope: Scope.REQUEST
})
export class TodoService {
}
```

注册 `TodoService` 服务：

```typescript
import { Module, Scope } from '@nestjs/common';

@Module({
  providers: [TodoService],
})
export class AppModule {}
```

请求 `TodoService` 服务：

```typescript
export class ToDoController {
  constructor(private readonly todoService: TodoService) {}
}
```

NestJs 还提供了在注册服务时指定对象的生存期的方案：

```typescript
export class TodoService {
}
```

注册 `TodoService` 服务：

```typescript
import { Module, Scope } from '@nestjs/common';

@Module({
  providers: [
    {
      provide: 'TODO_SERVICE',
      useClass: TodoService,
      scope: Scope.REQUEST
    }
  ],
})
export class AppModule {}
```

请求 `TodoService` 服务：

```typescript
import { Inject } from '@nestjs/common';

export class ToDoController {
  constructor(
    @Inject('TODO_SERVICE')
    private readonly todoService: TodoService,
  ) {}
}
```

### 获取 Request、Response

在 NestJs 中提供了两种方法获取 `Request` 与 `Response`:

- `@Req()`、`@Res()`: 在控制器中使用装饰器获取
- `@Inject()`: 通过依赖注入的方式获取

#### 使用装饰器获取

```typescript
import { Get, Req } from '@nestjs/common';
import { Request } from 'express';

export class TodoController {
	@Get()
	get(@Req() req: Request) {
    console.log(req.url);
	}
}
```

#### 使用依赖关系注入获取

```typescript
import { Inject }from '@nestjs/common';
import { REQUEST } from '@nestjs/core';
import { Request } from 'express';

export class ToDoController {
	constructor(
		@Inject(REQUEST)
		private readonly req: Request,
	) {}
}
```

## Web Api 应用

### Swagger/OpenApi

#### 安装

```typescript
npm install --save @nestjs/swagger swagger-ui-express
```

#### 添加并配置 Swagger 中间件

```typescript
import { SwaggerModule, DocumentBuilder } from '@nestjs/swagger';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  // 初始化 Swagger
  const config = new DocumentBuilder()
    .setTitle('Todo API')
    .setDescription('A simple example nodejs Web API')
    .setVersion('v1')
    .build();
  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api', app, document);

  await app.listen(3000);
}
bootstrap();
```

启动应用，并导航到 `http://localhost:3000/api`，即可通过 Swagger UI 浏览 API。

#### 接口分组

将 `ApiTag` 装饰器添加到控制器：

```typescript
import { ApiTags } from '@nestjs/swagger';

@ApiTags('Todo')
export class TodoController {
}
```

#### 参数注释

设置 `Query` 参数的注释，将 `ApiQuery` 装饰器添加到函数，`isArray` 指定参数为数组，`required` 指定参数为必填：

```typescript
import { ApiQuery } from '@nestjs/swagger';
import { Query } from '@nestjs/common';

@ApiQuery({
  name: 'id',
  description: `User's id`,
  type: Number,
  isArray: true,
  required: true,
})
getById(@Query('id') id: number[]) {
}
```

设置 `ApiBody` 参数的注释，将 `ApiQuery` 装饰器添加到函数：

```typescript
export class CreateUserDto {
  @ApiProperty({
    description: `User's name`,
    type: String,
  })
  name: string;
}
```

```typescript
import { ApiBody } from '@nestjs/swagger';
import { Body } from '@nestjs/common';

@ApiBody({
  type: CreateUserDto,
})
create(@Body() createUserDto: CreateUserDto) {
}
```

#### 上传文件注释

只允许上传单个文件，将 `ApiConsumes` 装饰器添加到函数，并指定为 `multipart/form-data`，将 `type` 指定为 `file`，`format` 指定为 `binary` 表示为二进制数据，`required` 指定参数为必填：

```typescript
import { ApiConsumes, ApiBody } from '@nestjs/swagger';
import { FileInterceptor } from '@nestjs/platform-express';
import { UseInterceptors, UploadedFile } from '@nestjs/common';

@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: 'object',
    properties: {
      file: {
        description: '文件的二进制数据',
        type: 'file',
        format: 'binary',
      },
    },
    required: ['file'],
  },
})
@UseInterceptors(FileInterceptor('file'))
upload(@UploadedFile() file: Express.Multer.File) {
}
```

允许上传多个文件，将 `ApiConsumes` 装饰器添加到函数，并指定为 `multipart/form-data`，将 `type` 指定为 `file`，`format` 指定为 `binary` 表示为二进制数据，`nullable` 指定参数是否可空的，`required` 指定参数为必填：

```typescript
import { ApiConsumes, ApiBody } from '@nestjs/swagger';
import { FileFieldsInterceptor } from '@nestjs/platform-express';
import { UploadedFile, UseInterceptors } from '@nestjs/common';

@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: object,
    properties: {
      ['readme.md']: {
        description: 'readme.md 文件的二进制数据',
        type: 'file',
        format: 'binary',
        nullable: true,
      },
      ['index.docx']: {
        description: 'index.docx 文件的二进制数据',
        type: 'file',
        format: 'binary',
        nullable: false,
      },
    },
    required: ['index.docx'],
  },
})
@UseInterceptors(
  FileFieldsInterceptor([
    { name: 'readme.md', maxCount: 1 },
    { name: 'index.docx', maxCount: 1 },
  ])
)
upload(
  @UploadedFile('readme.md') readme: Express.Multer.File,
  @UploadedFile('index.docx') docx: Express.Multer.File,
) {
}
```

允许上传任意文件，将 `ApiConsumes` 装饰器添加到函数，并指定为 `multipart/form-data`，将 `type` 指定为 `array` 表示属性为数组格式，将 `items` 中的 `type` 指定为 `file`，`format` 指定为 `binary` 表示为二进制数据；`required` 指定属性为必填：

```typescript
import { ApiConsumes, ApiBody } from '@nestjs/swagger';
import { AnyFilesInterceptor } from '@nestjs/platform-express';
import { UploadedFiles, UseInterceptors } from '@nestjs/common';

@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: object,
    properties: {
      ['files']: {
        type: 'array',
        description: '文件的二进制数据',
        items: {
          type: 'file',
          format: 'binary',
        },
      },
    },
    required: ['files'],
  },
})
@UseInterceptors(AnyFilesInterceptor())
upload(@UploadedFiles() files: Express.Multer.File[]) {
}
```

### 处理 form-data 请求

安装

```shell
npm install express-form-data
```

创建 `FormDataInterceptor` 拦截器，实现 `NestInterceptor` 接口：

```typescript
import {
  CallHandler,
  ExecutionContext,
  Injectable,
  NestInterceptor,
} from '@nestjs/common';
import { Observable, Subject } from 'rxjs';
import * as os from 'os';
import * as FormData from 'express-form-data';

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

接下来，将 `UseInterceptors` 装饰器添加到函数，即可处理 `form-data` 的请求：

```typescript
import { Post, Body, UseInterceptors } from '@nestjs/common';

export class TodoController {
  @Post()
  @UseInterceptors(FormDataInterceptor)
  post(@Body("name") name: string) {
  }
}
```

## 高级

### 下载二进制数据到客户端

```typescript
import { Get, Res } from "@nestjs/common";
import { Response } from "express";
import { Readable } from "stream";

export class TodoController {
  @Get()
  export(@Res() res: Response): void {
    const buffer = this.toDoService.getBuffer();
    
    const stream = new Readable();
    stream.push(buffer);
    stream.push(null);

    const filename = 'export.docx';

    res.set({
      "Content-Type": "application/octet-stream",
      "Content-Length": buffer.length,
      "Content-Disposition": `attachment; filename="${encodeURI(filename)}"`, // 指定下载的文件名称
    });

    stream.pipe(res);
  }
}
```