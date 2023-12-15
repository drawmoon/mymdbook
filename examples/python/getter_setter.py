class User:
    __name: str

    @property
    def name(self) -> str:
        print("Getter")
        return self.__name

    @name.setter
    def name(self, name: str):
        print("Setter")
        self.__name = name


user = User()
user.name = "Tom"
print(user.name)
