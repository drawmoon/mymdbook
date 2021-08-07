from typing import Dict

some_dict = {}
some_dict["a"] = 1
some_dict["b"] = 2

print(some_dict)
print(some_dict["a"])
[print(p) for p in some_dict]


some_dict2 = {
  "a": 1,
  "b": 2
}


some_dict3: Dict[str, int] = dict()
some_dict3["a"] = 1
some_dict3["b"] = 2

some_dict4 = {}

[some_dict4.update({ p: some_dict3[p] }) for p in ["a", "b"]]
print(some_dict4)

some_dict4.pop("a")
print(some_dict4)

some_dict4.clear()
print(some_dict4)


def print_dict(**dict):
  print(dict)

print_dict(a = 1, b = 2)
print_dict(**{ "a": 1, "b": 2 })


some_dict5 = {}
some_dict5[5] = "Kaitlin"
some_dict5[5.0] = "Yuvaan"
some_dict5[5.5] = "Munoz"

print(some_dict5) # 输出结果: {5: 'Yuvaan', 5.5: 'Munoz'}
print(5 == 5.0, hash(5) == hash(5.0)) # True True
