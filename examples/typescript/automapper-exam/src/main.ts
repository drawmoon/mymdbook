import { mapper } from './mapper';
import { User } from './user';
import { UserDTO } from './user.dto';

// 对象映射
const user: User = {
  id: 1,
  firstName: 'hu',
  lastName: 'hongqi',
  email: 'hongqi.hu@email.com',
  password: '123456',
};

const userDto = mapper.map(user, User, UserDTO);
console.log(userDto);

// 数组对象映射
const users: User[] = [user];

const userDtoList = mapper.mapArray(users, User, UserDTO);
console.log(userDtoList);

// 在旧的版本中
// const userDto = mapper.map(user, UserDTO, User);
// console.log(userDto);

// // 数组对象映射
// const users: User[] = [user];

// const userDtoList = mapper.mapArray(users, UserDTO, User);
// console.log(userDtoList);
