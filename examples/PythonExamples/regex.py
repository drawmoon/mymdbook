import re

pattern = "^[前后]?(?P<digit>[0-9零一二两三四五六七八九十])个?(?:[年月日]|星期|季度)以?[前后]?$"

match = re.search(pattern, "前两个星期")

if match is not None:
    digit = match.group("digit")
    print(digit)
