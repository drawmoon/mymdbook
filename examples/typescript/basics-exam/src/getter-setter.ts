class User {
  name: string;

  surname: string;

  get fullName(): string {
    return `${this.surname}${this.name}`;
  }
}

const user = new User();
console.log(user.fullName);
