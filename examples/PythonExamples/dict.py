from typing import Dict

some_dict = {}
some_dict["a"] = 1
some_dict["b"] = 2

print(some_dict)
print(some_dict["a"])  # 如果 key 不存在，则报错
print(some_dict.get("a"))  # 如果 key 不存在，则返回 None
[print(p) for p in some_dict]

some_dict2 = {
    "a": 1,
    "b": 2
}

some_dict3: Dict[str, int] = dict()
some_dict3["a"] = 1
some_dict3["b"] = 2

some_dict4 = {}

[some_dict4.update({p: some_dict3[p]}) for p in ["a", "b"]]
print(some_dict4)

# 删除字典元素
some_dict4 = {"a": 1, "b": 2, "c": 3, "d": 4}
some_dict4.pop("a")
print(some_dict4)

del some_dict4["b"]
print(some_dict4)

# 删除最后一个元素
some_dict4.popitem()
print(some_dict4)

some_dict4.clear()
print(some_dict4)


def print_dict(**val):
    print(val)


print_dict(a=1, b=2)
print_dict(**{"a": 1, "b": 2})

some_dict5 = {}
some_dict5[5] = "Kaitlin"
some_dict5[5.0] = "Yuvaan"
some_dict5[5.5] = "Munoz"

print(some_dict5)  # 输出结果: {5: 'Yuvaan', 5.5: 'Munoz'}
print(5 == 5.0, hash(5) == hash(5.0))  # True True

for (k, v) in some_dict.items():
    print(k, v)

# Dict to str
some_cookies = {"reqid": "20210809", "sid": "12345670"}
cookie_str = ";".join([f"{key}={val}" for (key, val) in some_cookies.items()])
print(cookie_str)

# 不存在则新增
some_dict.setdefault("a", 6)
some_dict.setdefault("c", 5)
print(some_dict)  # {'a': 1, 'b': 2, 'c': 5}

some_keys = ["Kaitlin", "Yuvaan", "Munoz"]
print(dict.fromkeys(some_keys, None))

print(len(some_dict))


# 合并
a_dict = {"a": 1, "b": 2}
b_dict = {"c": 3}
print(dict(a_dict, **b_dict))

# 合并字典中的集合
import itertools
test_dict = {"a": [1, 2, 3], "b": [4, 5, 6]}
print(list(itertools.chain.from_iterable(test_dict.values())))
