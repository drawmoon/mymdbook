# Table of contents

- [NestJs Notes](#nestjs-notes)
  - [处理 form-data 的请求](#处理-form-data-的请求)
  - [Swagger 指定 Query 参数为数组](#swagger-指定-query-参数为数组)
  - [Swagger 指定 form-data 参数为数组](#swagger-指定-form-data-参数为数组)

# NestJs Notes

## 处理 form-data 的请求

```ts
npm install express-form-data

// form-data.interceptor.ts
import { Injectable, NestInterceptor, ExecutionContext, CallHandler } from '@nestjs/common';
import { Observable, Subject } from 'rxjs';
import * as os from 'os';
import * as formData from 'express-form-data';

@Injectable()
export class FormDataInterceptor implements NestInterceptor {
  intercept(context: ExecutionContext, next: CallHandler): Observable<any> {
    const options = {
      uploadDir: os.tmpdir(),
      autoClean: true
    };

    const http = context.switchToHttp();
    const req = http.getRequest();
    const res = http.getResponse();

    const sub = new Subject();

    formData.parse(options)(req, res, () => {
      next.handle().subscribe(sub);
    });

    return sub;
  }
}

// folder.controller.ts
@Put(':id/name')
@ApiConsumes('multipart/form-data')
@UseInterceptors(FormDataInterceptor)
updateName(@Param('id') id: number, @Body('newName') newName: string): FolderDTO {
}
```

## Swagger 指定 Query 参数为数组

```ts
// folder.controller.ts
@Delete('batch')
@ApiQuery({
  name: 'ids',
  description: '批量删除的文件夹的Id',
  required: true,
  type: [Number],
  isArray: true,
  style: 'form',
  explode: true,
})
batchDelete(@Query('ids', ParseArrayPipe) ids: number[]): FolderDTO[] {
}
```

## Swagger 指定 form-data 参数为数组

```ts
// folder-batch-delete.dto.ts
@ApiProperty({
  description: '批量删除的文件夹的Id',
  type: Number,
  isArray: true,
  required: true,
})
ids: number[];

// folder.controller.ts
@Delete()
@ApiBody({
  type: UserBatchDeleteDto,
})
batchDelete(@Body('ids', ParseArrayPipe) ids: number[]): FolderDTO[] {
}
```
