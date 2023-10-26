import { Module } from '@nestjs/common';
import { FooModule } from './foo/foo.module';
import { SharedModule } from './shared/shared.module';

@Module({
  imports: [
    SharedModule,
    FooModule,
  ],
  providers: [],
})
export class AppModule {}
