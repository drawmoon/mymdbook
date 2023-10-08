import { Controller, Get } from '@nestjs/common';
import { FooService } from './foo.service';

@Controller()
export class FooController {
  constructor(private readonly fooService: FooService) {}

  @Get()
  get(): string {
    return this.fooService.get();
  }
}
