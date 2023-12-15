some_list = ["Mimi Randall", "Damien Kane", "Keiron Partridge"]
some_list[0] = "Marwah Grant"
print(some_list)


# 列表截取
some_name = some_list[1]
print(some_name)
some_name2 = some_list[-1]
print(some_name2)


# 从集合指定索引位置开始取值
some_names = some_list[1:]
print(some_names)  # 输出结果: ['Damien Kane', 'Keiron Partridge']


# 从集合末尾的指定索引位置开始取值
some_names2 = some_list[-2:]
print(some_names2)  # 输出结果: ['Damien Kane', 'Keiron Partridge']


# 步长，从索引 0 处开始取值，每两个索引取第一个索引的值
some_names3 = some_list[::2]
print(some_names3)  # 输出结果: ['Marwah Grant', 'Keiron Partridge']


# 获取集合长度
list_len = len(some_list)
print("length:", list_len)


# 追加元素
some_list.append("Archibald Hills")
print("append:", some_list)


# 追加集合
some_list.extend(["Bertie Mooney", "Abdul Hobbs"])
print("extend:", some_list)


# 在指定索引处添加元素
some_list.insert(0, "Brandi Preece")
print("insert:", some_list)


# 移除元素
some_list.remove("Archibald Hills")
print("remove:", some_list)


# 移除指定索引处的元素
del some_list[-1]
print("del:", some_list)


# 移除并返回指定索引处的元素
pop = some_list.pop(-1)
print("pop:", pop, "original list:", some_list)


# 获取元素的索引
index = some_list.index("Damien Kane")
print("index:", index)


# 反转集合
some_list.reverse()
print("reverse: ", some_list)
print("reverse: ", some_list[::-1])


# 排序
some_list.sort()
print("sort:", some_list)
some_list.sort(reverse=True)
print("sort(desc):", some_list)


# 统计元素出现的次数
some_names4 = [1, 1, 1, 2, 3]
print("count:", some_names4.count(1))


# 判断元素是否存在
exist = "Damien Kane" in some_list
print("in:", exist)


# 判断两个集合是否相等
listA = [1, 2, 3, 4]
listB = [1, 2, 3, 4]

result = listA == listB
print("equals:", result)

import operator
operator.eq(listA, listB)
print("operator.eq:", result)


# 判断集合 A 是否包含集合 B 的所有元素
listA = [1, 2, 3, 4]
listB = [1, 2, 3]
result = all(item in listA for item in listB)
print("all:", result)


# 判断集合 A 是否包含集合 B 的元素
listA = [1, 2, 3, 4]
listB = [4, 5, 6]
result = any(item in listA for item in listB)
print("any:", result)


# 取最大值
digit_list = [2, 54, 12, 45, 31, 6]
print("max:", max(digit_list))


# 取最小值
print("min:", min(digit_list))


# 求和
print("sum:", sum(digit_list))


# 浅克隆
original_list = [1, 2, ["Micah Mcphee", "Lleyton Connolly"]]
copied_list = original_list.copy()
print("original id:", id(original_list), "copied id:", id(copied_list))
copied_list[0] = "A"
copied_list[2][0] = "Reef Salinas"
print("original val:", original_list, "copied val:", copied_list)


# 深克隆
import copy
original_list = [1, 2, ["Micah Mcphee", "Lleyton Connolly"]]
deepcopied_list = copy.deepcopy(original_list)
print("original id:", id(original_list), "deepcopied id:", id(copied_list))
deepcopied_list[0] = "A"
deepcopied_list[2][0] = "Reef Salinas"
print("original val:", original_list, "deepcopied val:", copied_list)


# 集合去重
repeated_list = [1, 1, 2, 3, 4]
print(list(set(repeated_list)))


# 集合过滤查询
class User:
    id: str
    name: str
    age: int
    locked: bool

    def __init__(self, id: str, name: str, age: int, locked: bool) -> None:
        self.id = id
        self.name = name
        self.age = age
        self.locked = locked


user_list = [
    User("1", "Elysha Montes", 31, True),
    User("2", "Amman Rutledge", 33, True)
]

some_user = [u for u in user_list if u.id == "1"][0]
print(some_user)

some_user2 = next(filter(lambda u: u.id == "1", user_list))
print(some_user2)

some_user3 = list(filter(lambda u: u.locked, user_list))
print(some_user3)


# 转换为元组
print(tuple(some_list))


# 合并集合中的集合
import itertools
ts_list = [[1, 2, 3], [4, 5, 6]]
print(list(itertools.chain.from_iterable(ts_list)))
