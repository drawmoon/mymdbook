try:
    raise TypeError("类型错误")
except Exception as e:
    print(e)
else:
    print("没有报错时执行")
finally:
    print("始终执行")


try:
    print("Hello World")
except Exception as e:
    print(e)
else:
    print("没有报错时执行")
finally:
    print("始终执行")
