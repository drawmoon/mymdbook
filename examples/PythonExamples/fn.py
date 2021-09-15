from typing import Dict


# 函数的声明与调用
def print_msg(msg: str) -> None:
    print(msg)


print_msg("Hello world!")


# 删除函数
del print_msg
# print_msg("Hello world!")  # NameError: name 'print_msg' is not defined


# 内部函数
def hey():
    def come():
        print("hey come!")

    def follow():
        print("hey follow!")

    come()
    follow()


hey()


# 委托
def greet(make_greet):
    make_greet()


def en_greet():
    print("Hello!")


def cn_greet():
    print("你好！")


greet(en_greet)
greet(cn_greet)


# 匿名委托
greet(lambda: print("こんにちは!"))


# 剩余参数 *
def some_params_fn(s: str, *args):
    print(s)
    print(len(args))
    for arg in args:
        print(arg)


some_params_fn('hello', 1, 2, 3)


def some_fn(s: str, n: int):
    print(s, n)


def some_params_fn2(*args):
    some_fn(*args)


some_params_fn2('hello', 123)


# 剩余参数 **
def some_params_fn3(d: Dict, ner: str, **args):
    print("The value of parameter d is:", d)
    print("The value of parameter ner is:", ner)
    print("The value of parameter args is:", args)


some_dict = {"t": "fever", "ner": "cold"}
some_params_fn3(some_dict, **some_dict)
