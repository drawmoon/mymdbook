import { HttpService } from '@nestjs/axios';
import { Injectable } from '@nestjs/common';
import { catchError, lastValueFrom, map, of } from 'rxjs';

@Injectable()
export class AppService {
  constructor(private readonly httpService: HttpService) {}

  async get(): Promise<string[]> {
    const response = this.httpService
      .get('/api/categories')
      .pipe(
        map((x) => x.data),
        catchError((err) => {
          console.log(err);
          return of([]);
        })
      );
    return await lastValueFrom(response);
  }
}
