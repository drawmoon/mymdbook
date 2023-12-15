import re


rq_reg_exp = r"^(?P<prefix>[前后])?(?P<digit>[0-9零一二两三四五六七八九十])个?(?:[年月周日天]|星期|季度)以?(?P<suffix>[前后内])?$"

result = re.search(rq_reg_exp, "前两个星期")
if result is not None:
    digit = result.group("digit")
    print(digit)

# pattern
rq_pattern = re.compile(rq_reg_exp)
result = rq_pattern.search("前两个星期")
if result is not None:
    digit = result.group("digit")
    print(digit)


# 不区分大小写
str_pattern = re.compile(r"(a)", re.I)
result = str_pattern.search("ABC")
print(result.group(1))


# 将匹配到的值替换为新的值
str_pattern = re.compile(r"(B)")
print(str_pattern.sub("1", "ABC"))

str_pattern = re.compile(r"\*{3}(\S+)\*{3}")
print(str_pattern.sub(r"---\1---", "***ABC***"))


# 将匹配到的分组的值替换为新的值
input_str = """<figure class=3D"image"><img src=3D"file:///C:/fake/image0.png"> <figcaption>3D图形</figcaption>"""
htm_pattern = re.compile(r"<[^>]*=(?P<stain>3D)+\"\S+\"\s*\/?>")
repl_str = htm_pattern.sub(lambda x: input_str[x.start():x.end()].replace(x.group("stain"), ""), input_str)
print(repl_str)
