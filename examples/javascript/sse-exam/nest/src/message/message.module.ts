import { Module } from '@nestjs/common';
import { ClientNotifier } from './client-notifier';
import { MessageController } from './message.controller';

@Module({
  controllers: [MessageController],
  providers: [ClientNotifier],
})
export class MessageModule {}
