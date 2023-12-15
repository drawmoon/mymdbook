import { Get, Post, Req, Res } from '@nestjs/common';
import { Controller } from '@nestjs/common';
import { Request, Response } from 'express';
import { ClientNotifier } from './client-notifier';
import { EventBuilder } from './message-event';

@Controller('message')
export class MessageController {
  constructor(private readonly clientNotifier: ClientNotifier) {}

  @Get('connect')
  connect(@Req() req: Request, @Res() res: Response) {
    this.clientNotifier.connect(req, res);
  }

  @Post('send')
  send(@Req() req: Request) {
    const eventBuilder = new EventBuilder().setData({ ...req.body });
    this.clientNotifier.notify(eventBuilder, req);
  }
}
