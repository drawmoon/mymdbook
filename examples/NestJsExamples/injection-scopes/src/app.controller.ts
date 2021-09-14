import { Controller, Get, Req } from '@nestjs/common';
import { Request } from 'supertest';
import { AppService } from './app.service';
import { ScopeService } from './scope.service';

@Controller()
export class AppController {
  constructor(
    // 测试生命周期为 DEFAULT 的对象注入到生命周期为 REQUEST 的对象中时的效果
    // private readonly scopeService: ScopeService,
    private readonly appService: AppService,
  ) {
    console.log('进入 AppController 构造函数');
  }

  @Get()
  getHello(@Req() req: Request): string {
    return this.appService.getHello(req);
  }
}
