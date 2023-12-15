import { HttpModule } from '@nestjs/axios';
import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';

@Module({
  imports: [
    HttpModule.register({ baseURL: 'http://localhost:5000' }),
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
