import { NextFunction, Request, Response } from 'express';
import { Logger } from '@nestjs/common';

export function responseLoggingMiddleware(
  req: Request,
  res: Response,
  next: NextFunction,
) {
  const start = Date.now();

  res.on('close', () => {
    const end = Date.now();
    const totalElapsedTime = end - start;

    Logger.debug(
      `Process request '${req.method} - ${req.url}', total elapsed time: ${totalElapsedTime} ms.`,
    );
  });

  next();
}
