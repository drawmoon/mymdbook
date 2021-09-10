import re

str_pattern = r"^[前后]?(?P<digit>[0-9零一二两三四五六七八九十])个?(?:[年月日]|星期|季度)以?[前后]?$"

match = re.search(str_pattern, "前两个星期")
if match is not None:
    digit = match.group("digit")
    print(digit)

# pattern
pattern = re.compile(str_pattern)
match = pattern.search("前两个星期")
if match is not None:
    digit = match.group("digit")
    print(digit)

# 不区分大小写
pattern = re.compile(r"(a)", re.I)
match = pattern.search("ABC")
print(match.group(1))
