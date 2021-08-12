some_list = ["Kody Faulkner", "Mahi Kim", 100, "Lyla Harrington"]

has_digit = False
for item in some_list:
  if str(item).isdigit():
    has_digit = True

if not has_digit:
  print("No digit in list.")
else:
  print("Has digit in list.")

# 使用 for_else 优化代码
for item in some_list:
  if str(item).isdigit():
    break
else:
  print("No digit in list")
print("Has digit in list.")


has_digit = False
i = 0
while i < len(some_list):
  if str(some_list[i]).isdigit():
    has_digit = True
  i += 1

if not has_digit:
  print("No digit in list.")
else:
  print("Has digit in list.")

# 使用 while_else 优化代码
i = 0
while i < len(some_list):
  if str(some_list[i]).isdigit():
    break
  i += 1
else:
  print("No digit in list.")
print("Has digit in list.")