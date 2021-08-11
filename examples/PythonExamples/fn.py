from typing import Dict


def print_msg(msg: str) -> None:
  print(msg)

print_msg("Hello world!")



# Dict
def print_dict(d: Dict, ner, **args):
  print("The value of parameter d is:", d)
  print("The value of parameter ner is:", ner)
  print("The value of parameter args is:", args)


some_dict = {"t": "fever", "ner": "cold"}
print_dict(some_dict, **some_dict)
