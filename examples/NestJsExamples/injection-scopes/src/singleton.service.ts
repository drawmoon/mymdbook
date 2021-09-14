import { Injectable } from '@nestjs/common';

@Injectable()
export class SingletonService {
  constructor() {
    console.log('进入 SingletonService 构造函数');
  }
}
