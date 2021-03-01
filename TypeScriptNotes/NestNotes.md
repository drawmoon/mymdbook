# Table of contents

- [Nest Notes](#nest-notes)
  - [处理 form-data 的请求](#处理-form-data-的请求)
  - [接口执行文件下载操作](#接口执行文件下载操作)

# Nest Notes

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

## 接口执行文件下载操作

```ts
// file.controller.ts
import { Get, Res } from '@nestjs/common';
import { Response } from 'express';
import { Readable } from 'stream';

@Get('docx')
exportDocx(@Res() res: Response): void {
  const buffer = this.getDocxBuffer();

  this.download(res, buffer);
}

private download(res: Response, buffer: Buffer): void {
  const stream = new Readable();
  stream.push(buffer);
  stream.push(null);

  res.set({
    'Content-Type': 'application/octet-stream',
    'Content-Length': buffer.length,
    'Content-Disposition': 'attachment; filename="example.docx"', // 指定下载的文件名称
  });

  stream.pipe(res);
}
```
