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
装饰器 @catch_err 等同于 catch_err(hi)()
"""

fn = catch_err(hi)
fn()
