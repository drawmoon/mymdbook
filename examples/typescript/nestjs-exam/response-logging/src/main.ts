import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { responseLoggingMiddleware } from './middlewares/response-logging.middleware';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  app.use(responseLoggingMiddleware);

  await app.listen(3000);
}
bootstrap();
