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


# 将匹配到的值替换为新的值
pattern = re.compile(r"(B)")
print(pattern.sub("1", "ABC"))

pattern = re.compile(r"\*{3}(\S+)\*{3}")
print(pattern.sub(r"---\1---", "***ABC***"))


# 将匹配到的分组的值替换为新的值
input_str = """<figure class=3D"image"><img src=3D"file:///C:/fake/image0.png"> <figcaption>3D图形</figcaption>"""
pattern = re.compile(r"<[^>]*=(?P<stain>3D)+\"\S+\"\s*\/?>")
repl_str = pattern.sub(lambda x: input_str[x.start():x.end()].replace(x.group("stain"), ""), input_str)
print(repl_str)
