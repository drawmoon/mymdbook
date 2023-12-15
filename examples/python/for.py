some_list = ["Hello", "World"]


# for 语法
for item in some_list:
    print(item)


# enumerate 用于将可遍历的数组对象组合为索引序列，返回一个元组，包含元素所在的下标与元素的值
for i, item in enumerate(some_list):
    print(i, item)


# 列表生成式 (List Comprehensions)，基本语法：[exp for iter_var in iterable]
num_list = [1, 14, 5, 29, 48, 32, 41]
result = [num for num in num_list if num > 10]
print(result)


# For-Else，当循环全部执行完成后执行 else
for i in some_list:
    if i.isdigit():
        print("Has digit in list.")
        break
else:
    print("No digit in list.")


# While-Else，当循环全部执行完成后执行 else
i = 0
while i < len(some_list):
    if some_list[i].isspace():
        print("Has digit in list.")
        break
    i = i + 1
else:
    print("No digit in list.")
