import { Controller, Get, Post } from '@nestjs/common';
import { AppService } from './app.service';
import { Buckets } from 'aws-sdk/clients/s3';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  async getBuckets(): Promise<Buckets> {
    return await this.appService.listBucket();
  }

  @Get('assets')
  async getAssets(): Promise<void> {
    await this.appService.getAssets();
  }

  @Post('assets')
  async putAssets(): Promise<void> {
    await this.appService.putAssets();
  }
}
