import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UserAccessor, UserService } from './user.accessor';
import { APP_GUARD } from '@nestjs/core';
import { AuthGuard } from './auth.guard';
import { ScopeService } from './scope.service';
import { SingletonService } from './singleton.service';

const USER_ACCESSOR_PROVIDER = {
  provide: UserAccessor,
  useClass: UserService,
};

const APP_GUARD_PROVIDER = {
  provide: APP_GUARD,
  useClass: AuthGuard,
};

@Module({
  imports: [],
  controllers: [AppController],
  providers: [
    AppService,
    ScopeService,
    SingletonService,
    USER_ACCESSOR_PROVIDER,
    APP_GUARD_PROVIDER,
  ],
})
export class AppModule {}
