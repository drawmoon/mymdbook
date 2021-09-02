from datetime import datetime
from dateutil.relativedelta import relativedelta
import hanlp
from lark import Lark, Visitor, Tree, Token
from typing import Dict
import os

CN_DIGIT = {"零": 0, "一": 1, "二": 2, "两": 2, "三": 3, "四": 4, "五": 5, "六": 6, "七": 7, "八": 8, "九": 9, "十": 10}


def now():
    return datetime(2021, 9, 1)  # if os.getenv("ENVIRONMENT") == "TEST" else date.today()


def process_input(text):
    print("输入的字符:", text)

    han_lp = hanlp.load(hanlp.pretrained.mtl.CLOSE_TOK_POS_NER_SRL_DEP_SDP_CON_ELECTRA_SMALL_ZH)
    doc = han_lp([text])

    ner = doc["ner/ontonotes"]
    dt_text = ""
    for i, (word, typ, s, e) in enumerate(ner[0]):
        if i == 0:
            dt_text = word
            continue
        if word in ["当天"]:
            dt_text += word

    print("查找到的日期:", dt_text)
    return dt_text


class DateTreeVisitor(Visitor):
    date_dict: Dict[str, str]

    def __init__(self):
        self.date_dict = {"hour": "00", "minute": "00", "second": "00"}

    def years(self, tree: Tree):
        self.date_dict["year"] = self.__scan_values__(tree)

    def months(self, tree: Tree):
        today = now()
        keys = self.date_dict.keys()
        if "year" not in keys:
            self.date_dict["year"] = str(today.year)
        self.date_dict["month"] = self.__scan_values__(tree)

    def days(self, tree: Tree):
        today = now()
        keys = self.date_dict.keys()
        if "year" not in keys:
            self.date_dict["year"] = str(today.year)
        if "month" not in keys:
            self.date_dict["month"] = str(today.month).rjust(2, "0")
        self.date_dict["day"] = self.__scan_values__(tree)

    @staticmethod
    def __scan_values__(tree: Tree):
        val = ""
        for child in tree.children:
            if not isinstance(child, Token):
                raise TypeError("子节点不是 Token")
            val += child.value
        return val


def date_str_to_digit(s, typ):
    if typ == "year":
        if s.isdigit():
            return int(s) if len(s) == 4 else int(now().year / 100) * 100 + int(s)
        s = "".join([str(CN_DIGIT[c]) for c in s if c in CN_DIGIT.keys()])
        if s.isdigit():
            return int(s) if len(s) == 4 else int(now().year / 100) * 100 + int(s)
        else:
            return None
    else:
        if s.isdigit():
            return int(s)
        two_digit = 1
        digit = 0
        for c in s[::-1]:
            if c in CN_DIGIT.keys():
                tmp = CN_DIGIT[c]
                if tmp >= 10:
                    two_digit = tmp
                else:
                    digit += tmp * two_digit
            else:
                return None
        if digit < two_digit:
            digit += two_digit
        return digit


def parse_cn_date(dt_str):
    switch = {
        "今年": lambda n: n.year,
        "明年": lambda n: (n + relativedelta(years=1)).year,
        "去年": lambda n: (n - relativedelta(years=1)).year,
        "前年": lambda n: (n - relativedelta(years=2)).year,
        "本月": lambda n: n.month,
        "上月": lambda n: (n - relativedelta(months=1)).month,
        "下月": lambda n: (n + relativedelta(months=1)).month,
        "今天": lambda n: n,
        "明天": lambda n: n + relativedelta(days=1),
        "后天": lambda n: n + relativedelta(days=2),
        "昨天": lambda n: n - relativedelta(days=1),
        "前天": lambda n: n - relativedelta(days=2),
    }
    if dt_str not in switch:
        return []
    val = switch[dt_str](now())
    if dt_str.endswith("年"):
        return build_date(val)
    if dt_str.endswith("月"):
        return build_date(month=val)
    if dt_str.endswith("天"):
        return build_date(val.year, val.month, val.day)
    return []


def build_date(year: int = None, month: int = None, day: int = None, **keyword):
    if day is not None:
        start_date = datetime(year, month, day)
        end_date = start_date + relativedelta(days=1)
        return [start_date, end_date]
    if year is None and month is not None:
        today = now()
        start_date = datetime(today.year, month, 1)
        end_date = start_date + relativedelta(months=1)
        return [start_date, end_date]
    if year is not None and month is not None:
        start_date = datetime(year, month, 1)
        end_date = start_date + relativedelta(months=1)
        return [start_date, end_date]
    if year is not None:
        start_date = datetime(year, 1, 1)
        end_date = start_date + relativedelta(years=1)
        return [start_date, end_date]
    return []


def parse_datetime(dt_str):
    rst = parse_cn_date(dt_str)
    if len(rst) != 0:
        return tuple([s.strftime("%Y-%m-%d %H:%M:%S") for s in rst])
    date_parser = Lark(r"""
        start: date
        date : (date_full | date_month_day | years? months? days?) "当天"?
        
        date_full: years months days
        date_month_day: months days
        
        years   : DIGIT DIGIT (DIGIT DIGIT)? ("年" | "-" | "/")
        months  : DIGIT DIGIT? ("月" | "月份" | "-" | "/")
        days    : DIGIT (DIGIT DIGIT?)? ("日" | "号")?
        
        DIGIT: /["0-9零一二两三四五六七八九十"]/
        
        // Disregard spaces in text
        %ignore " "
    """)
    parse_tree = date_parser.parse(dt_str)
    visitor = DateTreeVisitor()
    visitor.visit(parse_tree)

    params = {}
    for key, val in visitor.date_dict.items():
        digit = date_str_to_digit(val, key)
        if digit is not None:
            params[key] = digit
    rst = build_date(**params)
    return None if len(rst) == 0 else tuple([s.strftime("%Y-%m-%d %H:%M:%S") for s in rst])


input_text = "帮我查看一下二零一七年七月二十三日当天购买了什么"

date_text = process_input(input_text)
rst = parse_datetime(date_text)
print("解析为查询条件:", f"时间 >= '{rst[0]}' and 时间 < '{rst[1]}'")
