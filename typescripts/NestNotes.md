# Table of contents

- [Nest Notes](#nest-notes)
  - [处理 form-data 的请求](#处理-form-data-的请求)
  - [接口执行文件下载操作](#接口执行文件下载操作)

# Nest Notes

## 处理 form-data 的请求

通过`npm`安装依赖项`npm install express-form-data`，或通过`yarn`安装依赖项`yarn add express-form-data`。

```ts
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
import { download } from './mvc-helper';

@Get('docx')
exportDocx(@Res() res: Response): void {
  const buffer = this.fileService.getDocxBuffer();

  download(res, buffer, 'example.docx');
}

// mvc-helper.ts
import { Response } from 'express';
import { Readable } from 'stream';

export function download(res: Response, buffer: Buffer, filename: string): void {
  const stream = new Readable();
  stream.push(buffer);
  stream.push(null);

  res.set({
    'Content-Type': 'application/octet-stream',
    'Content-Length': buffer.length,
    'Content-Disposition': `attachment; filename="${encodeURI(filename)}"`, // 指定下载的文件名称
  });

  stream.pipe(res);
}
```
