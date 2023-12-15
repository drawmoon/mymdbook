import { AutoMap } from '@automapper/classes';

export class UserDTO {
  @AutoMap()
  id: number;

  @AutoMap()
  fullName: string;

  @AutoMap()
  email: string;
}