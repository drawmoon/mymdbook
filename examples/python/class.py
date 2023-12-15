from typing import overload, Union, Iterable


# 类的声明
class Store:
    # 具有以 __ 前后缀的函数称为魔术函数，例如：__init__, __new__,

    # 初始化函数，构造实例的时候被调用
    def __init__(self, name: str):
        print("Store __init__ method will be called")

        # 私有字段
        self.__name = name

    # 当类实例化时被调用，在 __init__ 函数之前被调用，基类中声明了 __new__ 函数，子类中也必须声明 __new__ 函数
    # def __new__(cls, name: str):
    #     print("Store __new__ method will be called")
    #
    #     return super(Store, cls).__new__(cls)

    # 使实例可以以调用函数的方式被调用
    def __call__(self) -> None:
        print("Store __call__ method will be called")

    # 当实例被转换为字符串时被调用
    def __str__(self) -> str:
        return self.__name

    # 当调用 dir() 函数时被调用
    def __dir__(self) -> Iterable[str]:
        print("Store __dir__ method will be called")
        return ["get_name"]

    # 析构函数，销毁实例的时候被调用
    def __del__(self):
        print("Store __del__ method will be called")

    # 公共函数
    def get_name(self) -> str:
        return self.__name

    # 受保护的函数
    def _get_style(self) -> str:
        pass

    # 私有函数
    def __get_owner(self) -> str:
        pass


# 构造实例
store = Store("Game World")
# 调用 __call__ 函数
store()
# 调用 __str__ 函数
str(store)
# 调用 __dir__ 函数，获取实例的函数
dir(store)


# 类的继承
class PhysicalStore(Store):
    # 初始化函数重写
    def __init__(self, name: str, description: str):
        super(PhysicalStore, self).__init__(name)
        self.description = description

    # 函数重写
    def get_name(self) -> str:
        print("PhysicalStore get_name method will be called")
        return super().get_name()


# 函数重载
class UserAccessor:
    @overload
    def get_phone(self, uid: int) -> str: ...

    @overload
    def get_phone(self, name: str) -> str: ...

    def get_phone(self, option: Union[int, str]) -> str:
        return ''


user_accessor = UserAccessor()
user_accessor.get_phone(1)
user_accessor.get_phone("George")


# 判断或获取对象中指定的成员
class People:
    name = "Pikachu"

    def jog(self) -> None:
        pass


people = People()
print(hasattr(people, "name"), hasattr(people, "jog"))
print(getattr(people, "name"), getattr(people, "jog"))


# 在基类中调用子类的成员
class Foo:
    def __call__(self, member: str) -> None:
        try:
            getattr(self, member)()
        except AttributeError:
            print(f"Not Found attr {member}")


class Bar(Foo):
    def tell(self):
        print("tell method will be called")


bar = Bar()
bar("tell")
