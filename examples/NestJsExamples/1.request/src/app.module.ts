import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UserAccessor } from './user.accessor';

@Module({
  imports: [],
  controllers: [AppController],
  providers: [AppService, UserAccessor],
})
export class AppModule {}
