class UserService:
    name: str

    def __init__(self, name: str):
        print("UserService init")
        self.name = name

    def __call__(self, todo: str):
        print("UserService call")
        print(todo)

    def __del__(self):
        print("UserService del")

    def get_name(self) -> str:
        return self.name


user_service = UserService("Aniya Ford")
user_service("run")
user_name = user_service.get_name()
print(user_name)


# 继承、重写
class TUserService(UserService):
    def get_name(self) -> str:
        print("TUserService get_name")
        return super().get_name()


user_service = TUserService("Tom")
user_name = user_service.get_name()
print(user_name)


# 构造参数
class IdentUserService(UserService):
    tag: str

    def __init__(self, name: str, tag: str):
        super(IdentUserService, self).__init__(name)
        self.tag = tag


user_service = IdentUserService("Tom", "lib")
print(user_service.tag)


# 在父类中调用子类的方法
class Foo:
    def process(self, name: str):
        if hasattr(self, name):
            getattr(self, name)()
        else:
            print(f"Notfound attr {name}")


class Bar(Foo):
    def tell(self):
        print("Call tell fn")


bar = Bar()
bar.process("tell")
