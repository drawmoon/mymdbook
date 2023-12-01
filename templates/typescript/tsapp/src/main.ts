import { IVersion } from './interfaces';
import { Version } from './version';

export function version(): IVersion {
  return new Version(1, 0, 0);
}
