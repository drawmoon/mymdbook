# 无法在字符串中声明函数

s = "print(1 + 1)"
exec(s)

s = "1 + 1"
print(eval(s))


# 调用外部的函数
def add(x: int, y: int):
    return x + y


s = "add(1, 1)"
print(eval(s))
