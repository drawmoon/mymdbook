some_set = {1, 2, "a", True}

# 添加元素
some_set.add("Python")
print(some_set)


# 随机删除元素
some_set.pop()
print(some_set)


# 删除指定元素，当元素不存在时报错
some_set.remove("a")
print(some_set)


# 删除指定元素，当元素不存在时不进行任何操作
some_set.discard("b")
print(some_set)

some_set.clear()
print(some_set)

some_set1 = {"Mimi Randall", "Damien Kane", "Keiron Partridge"}
some_set2 = {"Bertie Mooney", "Mimi Randall", "Abdul Hobbs"}


# 交集 and
print(some_set1.intersection(some_set2))
print(some_set1 & some_set2)


# 并集 or
print(some_set1.union(some_set2))
print(some_set1 | some_set2)


# 差集 not，A 集合不存在 B 集合中的元素
print(some_set1.difference(some_set2))
print(some_set1 - some_set2)


# 对称差集，A 集合不存在 B 集合中的元素与 B 集合不存在 A 集合中的元素
print(some_set1.symmetric_difference(some_set2))
print(some_set1 ^ some_set2)


# 子级
sub_set = {"Mimi Randall", "Damien Kane"}
print(sub_set.issubset(some_set1))


# 父级
sup_set = {"Mimi Randall", "Damien Kane", "Keiron Partridge", "Abdul Hobbs"}
print(sup_set.issuperset(some_set1))


# 关系运算并修改
some_set3 = {"a", "b", "c"}
some_set4 = {"a", "d", "e"}
some_set3.intersection_update(some_set4)
# some_set3.difference_update(some_set4)
# some_set3.symmetric_difference_update(some_set4)
print(some_set3)
