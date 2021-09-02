from datetime import datetime
from dateutil.relativedelta import relativedelta
import hanlp
from lark import Lark, Visitor, Tree, Token
from typing import Dict
import os


def now():
    return datetime(2021, 9, 1)  # if os.getenv("ENVIRONMENT") == "TEST" else datetime.now()


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


class CnDateProcessor:
    @staticmethod
    def 今年():
        today = now()
        return build_date(today.year)

    @staticmethod
    def 明年():
        today = now()
        today += relativedelta(years=1)
        return build_date(today.year)

    @staticmethod
    def 去年():
        today = now()
        today -= relativedelta(years=1)
        return build_date(today.year)

    @staticmethod
    def 前年():
        today = now()
        today -= relativedelta(years=2)
        return build_date(today.year)

    @staticmethod
    def 本月():
        today = now()
        return build_date(month=today.month)

    @staticmethod
    def 下月():
        today = now()
        today += relativedelta(months=1)
        return build_date(month=today.month)

    @staticmethod
    def 上月():
        today = now()
        today -= relativedelta(months=1)
        return build_date(month=today.month)

    @staticmethod
    def 今天():
        today = now()
        return build_date(today.year, today.month, today.day)

    @staticmethod
    def 明天():
        today = now()
        today += relativedelta(days=1)
        return build_date(today.year, today.month, today.day)

    @staticmethod
    def 后天():
        today = now()
        today += relativedelta(days=2)
        return build_date(today.year, today.month, today.day)

    @staticmethod
    def 昨天():
        today = now()
        today -= relativedelta(days=1)
        return build_date(today.year, today.month, today.day)

    @staticmethod
    def 前天():
        today = now()
        today -= relativedelta(days=2)
        return build_date(today.year, today.month, today.day)

    @staticmethod
    def 上半年():
        today = now()
        start_date = datetime(today.year, 1, 1)
        end_date = datetime(today.year, 7, 1)
        return [start_date, end_date]

    @staticmethod
    def 下半年():
        today = now()
        start_date = datetime(today.year, 7, 1)
        today += relativedelta(years=1)
        end_date = datetime(today.year, 1, 1)
        return [start_date, end_date]

    @staticmethod
    def 第一季度():
        today = now()
        start_date = datetime(today.year, 1, 1)
        end_date = datetime(today.year, 4, 1)
        return [start_date, end_date]

    @staticmethod
    def 第二季度():
        today = now()
        start_date = datetime(today.year, 4, 1)
        end_date = datetime(today.year, 7, 1)
        return [start_date, end_date]

    @staticmethod
    def 第三季度():
        today = now()
        start_date = datetime(today.year, 7, 1)
        end_date = datetime(today.year, 10, 1)
        return [start_date, end_date]

    @staticmethod
    def 第四季度():
        today = now()
        start_date = datetime(today.year, 10, 1)
        today += relativedelta(years=1)
        end_date = datetime(today.year, 1, 1)
        return [start_date, end_date]


def str_to_digit(s, typ):
    cn_digit = {"零": 0, "一": 1, "二": 2, "两": 2, "三": 3, "四": 4, "五": 5, "六": 6, "七": 7, "八": 8, "九": 9, "十": 10}
    if typ == "year":
        if s.isdigit():
            return int(s) if len(s) == 4 else int(now().year / 100) * 100 + int(s)
        s = "".join([str(cn_digit[c]) for c in s if c in cn_digit.keys()])
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
            if c in cn_digit.keys():
                tmp = cn_digit[c]
                if tmp >= 10:
                    two_digit = tmp
                else:
                    digit += tmp * two_digit
            else:
                return None
        if digit < two_digit:
            digit += two_digit
        return digit


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


def parse_cn_date(dt_str):
    processor = CnDateProcessor()
    if hasattr(processor, dt_str):
        fn = getattr(processor, dt_str)
        return fn()
    return []


def parse(dt_str):
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
        digit = str_to_digit(val, key)
        if digit is not None:
            params[key] = digit
    rst = build_date(**params)
    return None if len(rst) == 0 else tuple([s.strftime("%Y-%m-%d %H:%M:%S") for s in rst])


input_text = "帮我查看一下二零一七年七月二十三日当天购买了什么"

date_text = process_input(input_text)
rst = parse(date_text)
print("解析为查询条件:", f"时间 >= '{rst[0]}' and 时间 < '{rst[1]}'")