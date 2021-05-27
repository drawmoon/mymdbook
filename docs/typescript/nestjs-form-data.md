# NestJs 处理 form-data 的请求

安装依赖包

```bash
npm install express-form-data

# Or
yarn add express-form-data
```

定义一个`FormDataInterceptor`拦截器，继承自`NestInterceptor`，并用`@Injectable()`装饰器标记`FormDataInterceptor`

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

在控制器的方法中绑定`FormDataInterceptor`拦截器

```typescript
export class FolderController {
  @Put(":id/name")
  @UseInterceptors(FormDataInterceptor)
  @ApiConsumes("multipart/form-data")
  updateName(
    @Param("id") id: number,
    @Body("newName") newName: string
  ): FolderDTO {
    // ...
  }
}
```

> 上面代码中`@ApiConsumes('multipart/form-data')`是 Swagger 提供的装饰器，用于描述接口的内容编码类型，这样就可以在 Swagger 中进行`form-data`接口的测试
