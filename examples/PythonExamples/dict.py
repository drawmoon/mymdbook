from typing import Dict

args = {}
args["a"] = 1
args["b"] = 2

print(args)
print(args["a"])
[print(p) for p in args]


args2 = {
  "a": 1,
  "b": 2
}


args3: Dict[str, int] = dict()
args3["a"] = 1
args3["b"] = 2

args4 = {}

[args4.update({ p: args3[p] }) for p in ["a", "b"]]
print(args4)

args4.pop("a")
print(args4)

args4.clear()
print(args4)


def print_arg(**args):
  print(args)

print_arg(a = 1, b = 2)
print_arg(**{ "a": 1, "b": 2 })
