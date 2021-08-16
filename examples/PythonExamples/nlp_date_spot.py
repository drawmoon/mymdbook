from datetime import datetime, timedelta, date
from dateutil.parser import parse
from dateutil.relativedelta import relativedelta
import hanlp
from lark import Lark, Visitor, Tree, Token


CN_DATE = {
    "今天": lambda: date.today(),
    "明天": lambda: date.today() + timedelta(days=1),
    "后天": lambda: date.today() + timedelta(days=2),
    "昨天": lambda: date.today() - timedelta(days=1),
    "前天": lambda: date.today() - timedelta(days=2),
    "本月": lambda: parse(date.today().strftime("%Y-%m")),
    "上月": lambda: (date.today() - relativedelta(months=1)).strftime("%Y-%m"),
    "下月": lambda: (date.today() + relativedelta(months=1)).strftime("%Y-%m"),
    "今年": lambda: date.today().year,
    "明年": lambda: (date.today() + relativedelta(years=1)).year,
    "去年": lambda: (date.today() - relativedelta(years=1)).year,
    "前年": lambda: (date.today() - relativedelta(years=2)).year,
}

CN_DIGIT = {'零': 0, '一': 1, '二': 2, '两': 2, '三': 3, '四': 4, '五': 5, '六': 6, '七': 7, '八': 8, '九': 9, '十': 10}


def process_input(text):
    print("输入的字符", text)

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

    print("查找到的日期", dt_text)
    return dt_text


class DateTreeVisitor(Visitor):
    date_dict = {"hour": "00", "minute": "00", "second": "00"}

    def years(self, tree: Tree):
        self.date_dict["year"] = self.__scan_values__(tree)

    def months(self, tree: Tree):
        today = date.today()
        keys = self.date_dict.keys()
        if "year" not in keys:
            self.date_dict["year"] = str(today.year)
        self.date_dict["month"] = self.__scan_values__(tree)

    def days(self, tree: Tree):
        today = date.today()
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
            return int(s) if len(s) == 4 else int(datetime.today().year / 100) * 100 + int(s)
        s = "".join([str(CN_DIGIT[c]) for c in s if c in CN_DIGIT.keys()])
        if s.isdigit():
            return int(s) if len(s) == 4 else int(datetime.today().year / 100) * 100 + int(s)
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


def parse_datetime(dt_str):
    try:
        dt = parse(dt_str)
        return dt.strftime('%Y-%m-%d %H:%M:%S')
    except:
        if dt_str in CN_DATE:
            target_date = CN_DATE[dt_str]()
            return target_date.strftime('%Y-%m-%d %H:%M:%S')

        date_parser = Lark(r"""
            start: date
            date : years? months? days? "当天"?

            years   : DIGIT DIGIT (DIGIT DIGIT)? ("年" | "-" | "/")
            months  : DIGIT DIGIT? ("月" | "-" | "/")
            days    : DIGIT (DIGIT DIGIT?)? "日"?

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
        target_date = datetime.today().replace(**params)
        return None if target_date is None else target_date.strftime("%Y-%m-%d %H:%M:%S")


# input_text = "帮我查看一下二零一七年七月二十三日当天购买了什么"
input_text = "帮我查看一下二零一七年购买了什么"

date_text = process_input(input_text)
print("解析的日期是", parse_datetime(date_text))
