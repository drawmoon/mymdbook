import { Module } from '@nestjs/common';
import { AppController } from './controllers/app.controller';
import { ScopeService } from './services/scope.service';
import { SingletonService } from './services/singleton.service';
import { TransientService } from './services/transient.service';

@Module({
  imports: [],
  controllers: [AppController],
  providers: [ScopeService, SingletonService, TransientService],
})
export class AppModule {}
