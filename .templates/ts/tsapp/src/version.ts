import { IVersion } from './interfaces';

/**
 * The default IVersion implementation.
 */
export class Version implements IVersion {
  constructor(readonly major: number, readonly minor: number, readonly build: number) {}
  
  public string(): string {
    return [this.major, this.minor, this.build].join('.');
  }
}
