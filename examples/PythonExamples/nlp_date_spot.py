from datetime import datetime, timedelta
from dateutil.parser import parse
import hanlp
import re

DATE_REG = r"([0-9零一二两三四五六七八九]{2,4}[年\-\/])?([0-9一二两三四五六七八九十]{1,2}[月\-\/])?([0-9一二两三四五六七八九十]{1,3}[号日])?([0-9零一二两三四五六七八九十百]{1,2}[点时:])?([0-9零一二三四五六七八九十百]{1,2}}分?)?([0-9零一二三四五六七八九十百]{1,2}秒)?"

DATE_KEY = {"今天": 0, "当天": 0, "明天": 1, "后天": 2}

CN_DIGIT = {'零': 0, '一': 1, '二': 2, '两': 2, '三': 3, '四': 4, '五': 5, '六': 6, '七': 7, '八': 8, '九': 9, '十': 10}


def process_input(text):
    print("输入的字符", text)

    han_lp = hanlp.load(hanlp.pretrained.mtl.CLOSE_TOK_POS_NER_SRL_DEP_SDP_CON_ELECTRA_SMALL_ZH)
    doc = han_lp([text])
    # print(doc)

    ner = doc["ner/ontonotes"]
    # print(ner)
    dt_text = ""
    for i, (word, typ, s, e) in enumerate(ner[0]):
        if i == 0:
            dt_text = word
            continue
        if word in DATE_KEY.keys():
            dt_text += word

    print("查找到的日期", dt_text)
    return dt_text


def date_str_to_digit(s, typ):
    for suf in ["年", "月", "日", "号", "时", "分", "秒", "/", "-", ":"]:
        if s.endswith(suf):
            s = s.removesuffix(suf)
            break
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
        target_date = ""
        m = re.match(DATE_REG, dt_str)
        if len(m.groups()) != 0:
            match_dates = m.groups(default="00")
            date_group = {}
            for i, k in enumerate(["year", "month", "day", "hour", "minute", "second"]):
                date_group[k] = match_dates[i]

            params = {}
            for group in date_group:
                digit = date_str_to_digit(date_group[group], group)
                if digit is not None:
                    params[group] = int(digit)
            target_date = datetime.today().replace(**params)
        return None if target_date is None else target_date.strftime('%Y-%m-%d %H:%M:%S')


input_text = "帮我查看一下二零一七年七月二十三日当天购买了什么"

date_text = process_input(input_text)
print("解析的日期是", parse_datetime(date_text))
