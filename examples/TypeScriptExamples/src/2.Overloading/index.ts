interface User {
  id: string;
}

export class UserService {
  getUser(id: string);
  getUser(options: (users: User[]) => User);
  getUser(id: string | ((users: User[]) => User), callback?: () => void) {
    console.log(id, callback);
  }
}

const userService = new UserService();
userService.getUser('1');
userService.getUser((us) => us[0]);
