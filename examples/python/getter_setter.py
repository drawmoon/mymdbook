class User:
    __name: str

    @property
    def name(self) -> str:
        return self.__name

    @name.setter
    def name(self, name: str):
        self.__name = name


user = User()
user.name = "Tom"
print(user.name)
