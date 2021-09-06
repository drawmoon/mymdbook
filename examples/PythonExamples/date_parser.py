from datetime import datetime
from dateutil.relativedelta import relativedelta
import hanlp
from lark import Lark, Visitor, Tree, Token
from typing import Dict, List, Tuple
import os


def now():
    # 方便进行单元测试
    return datetime(2021, 9, 1)  # if os.getenv("ENVIRONMENT") == "TEST" else datetime.now()


def process_input(text):
    print("输入的字符:", text)

    han_lp = hanlp.load(hanlp.pretrained.mtl.CLOSE_TOK_POS_NER_SRL_DEP_SDP_CON_ELECTRA_SMALL_ZH)
    doc = han_lp(text)

    ner = merge(doc["ner/ontonotes"])

    dt_text = ner[0][0]
    print("查找到的日期:", dt_text)
    return dt_text


def merge(lst: List[Tuple[str, str, int, int]]):
    merged = []
    slice_word = ""
    slice_s = 0
    for i, (word, typ, s, e) in enumerate(lst):
        if slice_word == "":
            slice_s = s
        nxt = lst[i + 1] if len(lst) > i + 1 else None
        # 如果没有相邻的或已经是最后一个元素
        if nxt is None or e != nxt[2]:
            if slice_word != "":
                merged.append((slice_word + word, typ, slice_s, e))
                slice_word = ""
            else:
                merged.append((word, typ, s, e))
        else:
            # 下一个元素的类型与当前的类型一致，需要合并
            if typ == nxt[1]:
                slice_word += word
            # 当前的类型是序数或整数并且下一个元素的类型是日期或时间，需要合并
            elif typ in ["ORDINAL", "INTEGER"] and nxt[1] in ["DATE", "TIME"]:
                slice_word += word
    return merged


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
        time = now()
        return build_date(time.year)

    @staticmethod
    def 明年():
        time = now()
        time += relativedelta(years=1)
        return build_date(time.year)

    @staticmethod
    def 去年():
        time = now()
        time -= relativedelta(years=1)
        return build_date(time.year)

    @staticmethod
    def 前年():
        time = now()
        time -= relativedelta(years=2)
        return build_date(time.year)

    @staticmethod
    def 本月():
        time = now()
        return build_date(month=time.month)

    @staticmethod
    def 下月():
        time = now()
        time += relativedelta(months=1)
        return build_date(month=time.month)

    @staticmethod
    def 上月():
        time = now()
        time -= relativedelta(months=1)
        return build_date(month=time.month)

    @staticmethod
    def 今天():
        time = now()
        return build_date(time.year, time.month, time.day)

    @staticmethod
    def 明天():
        time = now()
        time += relativedelta(days=1)
        return build_date(time.year, time.month, time.day)

    @staticmethod
    def 后天():
        time = now()
        time += relativedelta(days=2)
        return build_date(time.year, time.month, time.day)

    @staticmethod
    def 昨天():
        time = now()
        time -= relativedelta(days=1)
        return build_date(time.year, time.month, time.day)

    @staticmethod
    def 前天():
        time = now()
        time -= relativedelta(days=2)
        return build_date(time.year, time.month, time.day)

    @staticmethod
    def 上午():
        time = now()
        start_date = datetime(time.year, time.month, time.day)
        end_date = start_date.replace(hour=12)
        return [start_date, end_date]

    @staticmethod
    def 下午():
        time = now()
        start_date = datetime(time.year, time.month, time.day, 12)
        end_date = start_date.replace(hour=19)
        return [start_date, end_date]

    @staticmethod
    def 本周():
        time = now()
        start_date = time - relativedelta(days=time.weekday())
        time += relativedelta(weeks=1)
        end_date = time - relativedelta(days=time.weekday())
        return [start_date, end_date]

    @staticmethod
    def 上周():
        time = now()
        last_week = time - relativedelta(weeks=1)
        start_date = last_week - relativedelta(days=last_week.weekday())
        end_date = time - relativedelta(days=time.weekday())
        return [start_date, end_date]

    @staticmethod
    def 下周():
        time = now() + relativedelta(weeks=1)
        start_date = time - relativedelta(days=time.weekday())
        time += relativedelta(weeks=1)
        end_date = time - relativedelta(days=time.weekday())
        return [start_date, end_date]

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
        date : years? months? | ((years? months)? days) "当天"?
        
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


input_text = "第四季度总共有多少订单"

date_text = process_input(input_text)
rst = parse(date_text)
print("解析为查询条件:", f"时间 >= '{rst[0]}' and 时间 < '{rst[1]}'")
