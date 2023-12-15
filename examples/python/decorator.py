# 基于函数的装饰器
def catch_err(some_fn):
    def wrap():
        try:
            some_fn()
        except Exception as e:
            print(e)
    return wrap


@catch_err
def hi():
    print("Hello World")
    raise TypeError("类型报错")


hi()


"""
基于函数的装饰器 @catch_err 等同于 catch_err(hi)()
"""

fn = catch_err(hi)
fn()


# 基于类的装饰器
class Attr:
    tag: str = None

    def __init__(self, tag: str):
        print("__init__", "tag:", tag)
        self.tag = tag

    def __call__(self, fnc):
        print("__call__", "fnc:", fnc)
        fnc.__setattr__("__attribute__", self)
        return fnc


class Bootstrap:
    @Attr("startup")
    def startup(self):
        print("Hello World!")

    @Attr("restart")
    def restart(self):
        print("Restarting")


bootstrap = Bootstrap()
print(bootstrap.__dict__)  # {}

bootstrap.startup()
print(bootstrap.startup.__dict__)  # {'__attribute__': <__main__.Attr object at 0x00000227D6A82F70>}
print(bootstrap.startup.__dict__["__attribute__"].tag)  # startup

bootstrap.restart()
print(bootstrap.restart.__dict__)  # {'__attribute__': <__main__.Attr object at 0x0000013A82574C40>}
print(bootstrap.restart.__dict__["__attribute__"].tag)  # restart
