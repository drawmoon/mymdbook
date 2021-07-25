# NestJs 入坑指南

- [Swagger](#swagger)
- [获取 Request](#获取-request)
- [处理-form-data-请求](#处理-form-data-请求)
- [使用多个环境变量的配置文件](#使用多个环境变量的配置文件)
- [下载文件](#下载文件)

## Swagger

### 配置 Swagger

安装

```bash
npm install --save @nestjs/swagger swagger-ui-express
```

添加并配置 Swagger 中间件

```typescript
import { SwaggerModule, DocumentBuilder } from '@nestjs/swagger';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  // 初始化 Swagger
  const config = new DocumentBuilder()
    .setTitle('ToDo API')
    .setDescription('A simple example nodejs Web API')
    .setVersion('v1')
    .build();
  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api', app, document);

  await app.listen(3000);
}
bootstrap();
```

### 描述控制器

通过 `ApiTag` 装饰器描述控制器

```bash
import { ApiTags } from '@nestjs/swagger';

@ApiTags('ToDo')
export class TodoController {
}
```

### 描述数组参数

#### Query 数组参数

通过 `ApiQuery` 装饰器描述接口参数，`isArray` 属性指定参数是一个数组

```typescript
@ApiQuery({
  name: 'id',
  description: 'The param description',
  type: Number,
  isArray: true,
})
getById(
  @Query(
    'id',
    new ParseArrayPipe({
      items: Number,
      optional: false,
    })
  )
  id: number[]
) {
}
```

> `ParseArrayPipe` 是为了验证并将参数数据类型转换为 `Number`，`items` 指定数组元素的数据类型，`optional` 指定是否为可选参数

#### Body 数组参数

通过 `ApiProperty` 装饰器描述 `Body` 参数，`isArray` 属性指定参数是一个数组

```typescript
class GetUserByIdDTO {
  @ApiProperty({
    description: 'The param description',
    type: Number,
    isArray: true,
  })
  id: number[];
}

@ApiBody({
  type: GetUserByIdDTO,
})
getById(
  @Body(
    'id',
    new ParseArrayPipe({
      items: Number,
      optional: false,
    })
  )
  ids: number[]
) {
}
```

> `ParseArrayPipe` 是为了验证并将参数数据类型转换为 `Number`，`items` 指定数组元素的数据类型，`optional` 指定是否为可选参数

### 描述文件上传

#### 描述单个文件

`required` 指定属性是否必填，将属性的 `type` 指定为 `file`，`format` 指定为 `binary`，表示为二进制数据

```typescript
@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: 'object',
    required: ['file'],
    properties: {
      file: {
        description: '文件的二进制数据',
        type: 'file',
        format: 'binary',
      },
    },
  },
})
@UseInterceptors(FileInterceptor('file'))
upload(@UploadedFile() file: Express.Multer.File) {
}
```

> `ApiConsumes` 装饰器用来描述接口的请求类型

#### 描述多个文件

`required` 指定属性是否必填，将属性的 `type` 指定为 `file`，`format` 指定为 `binary`，表示为二进制数据，`nullable` 描述属性是否可空的

```typescript
@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: object,
    required: ['index.docx'],
    properties: {
      ['readme.md']: {
        description: 'Readme.md 二进制数据',
        type: 'file',
        format: 'binary',
        nullable: false,
      },
      ['index.docx']: {
        description: 'Index.docx 二进制数据',
        type: 'file',
        format: 'binary',
        nullable: true,
      },
    },
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

> `ApiConsumes` 装饰器用来描述接口的请求类型

#### 描述任意文件

`required` 指定属性是否必填，将 `items` 中的属性的 `type` 指定为 `file`，`format` 指定为 `binary`，表示为二进制数据

```typescript
@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: object,
    required: ['files'],
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
  },
})
@UseInterceptors(AnyFilesInterceptor())
upload(@UploadedFiles() files: Express.Multer.File[]) {
}
```

> `ApiConsumes` 装饰器用来描述接口的请求类型

## 获取 Request

### 通过装饰器获取 Request

```typescript
import { Req } from '@nestjs/common';
import { Request } from 'express';

getUser(@Req() req: Request) {
}
```

#### 通过依赖注入获取 Request

```typescript
import { Inject }from '@nestjs/common';
import { REQUEST } from '@nestjs/core';
import { Request } from 'express';

class ToDoService {
	constructor(
		@Inject(REQUEST)
		private readonly request: Request,
	) {}
}
```

## 处理 form-data 请求

安装

```bash
npm install express-form-data
```

定义拦截器 `FormDataInterceptor`，实现 `NestInterceptor` 类

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

使用 `UseInterceptors` 装饰器装饰方法

```typescript
export class ToDoController {
  @Post()
  @UseInterceptors(FormDataInterceptor)
  post(
    @Param("id") id: number,
    @Body("name") name: string
  ) {
  }
}
```

## 使用多个环境变量的配置文件

安装

```bash
npm install --save dotenv
```

在项目根目录新建配置文件，`.env` 用于生产环境的配置，`.development.env` 用于开发环境的配置。

修改 `package.json`：

```json
{
  "scripts": {
    "start": "node -r dotenv/config \"./node_modules/@nestjs/cli/bin/nest.js\" start dotenv_config_path=.development.env",
    "start:dev": "node --watch -r dotenv/config \"node_modules/@nestjs/cli/bin/nest.js\" start dotenv_config_path=.development.env",
    "start:prod": "node main",
    "start:inspect": "node --inspect=127.0.0.1:9229 main"
  }
}
```

执行 `npm run start` 使，使用的配置文件指定为了 `.development.env`；没有指定则默认使用 `.env`，所有执行 `node main` 时，使用的配置文件为 `.env`。

## 下载文件

### 返回二进制数据使浏览器执行下载

新建 `http-utilities.ts` 文件，声明 `download` 方法。

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

在控制器方法中调用 `download` 方法，让浏览器执行文件下载。

```typescript
import { Get, Res } from "@nestjs/common";
import { Response } from "express";
import { download } from "../shared/lib/http-utilities";

export class FileController {
  @Get("docx")
  exportDocx(@Res() res: Response): void {
    const buffer = this.fileService.getFile();
    download(res, buffer, "example.docx");
  }
}
```
