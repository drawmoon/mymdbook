import { Injectable } from '@nestjs/common';

@Injectable()
export class FooService {
  public get(): string {
    return new Date().toLocaleString();
  }
}
