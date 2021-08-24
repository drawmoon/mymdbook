import unittest
from nlp_date_spot import date_str_to_digit, parse_datetime
from datetime import date
import calendar


def v(val: int, c: int = 0):
    return str(val + c).rjust(2, "0")


def month_last_day(year: int, month: int):
    last = calendar.monthrange(year, month)[1]
    return f"{year}-{v(month)}-{last}"


class NlpDateSpotTest(unittest.TestCase):
    def test_date_str_to_digit(self):
        test_str = "17"
        d = date_str_to_digit(test_str, "year")
        self.assertEqual(d, 2017)

        test_str = "2017"
        d = date_str_to_digit(test_str, "year")
        self.assertEqual(d, 2017)

        test_str = "一七"
        d = date_str_to_digit(test_str, "year")
        self.assertEqual(d, 2017)

        test_str = "二零一七"
        d = date_str_to_digit(test_str, "year")
        self.assertEqual(d, 2017)

        test_str = "07"
        d = date_str_to_digit(test_str, "year")
        self.assertEqual(d, 2007)

        test_str = "零七"
        d = date_str_to_digit(test_str, "year")
        self.assertEqual(d, 2007)

        test_str = "12"
        d = date_str_to_digit(test_str, "month")
        self.assertEqual(d, 12)

        test_str = "十二"
        d = date_str_to_digit(test_str, "month")
        self.assertEqual(d, 12)

        test_str = "1"
        d = date_str_to_digit(test_str, "month")
        self.assertEqual(d, 1)

        test_str = "一"
        d = date_str_to_digit(test_str, "month")
        self.assertEqual(d, 1)

        test_str = "31"
        d = date_str_to_digit(test_str, "day")
        self.assertEqual(d, 31)

        test_str = "三十一"
        d = date_str_to_digit(test_str, "day")
        self.assertEqual(d, 31)

        test_str = "1"
        d = date_str_to_digit(test_str, "day")
        self.assertEqual(d, 1)

        test_str = "一"
        d = date_str_to_digit(test_str, "day")
        self.assertEqual(d, 1)

    def test_parse_datetime(self):
        now = date.today()

        test_str = "2017/7/23"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], "2017-07-23 00:00:00")

        test_str = "2017-7-23"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], "2017-07-23 00:00:00")

        test_str = "2017年7月23日"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], "2017-07-23 00:00:00")

        test_str = "二零一七年七月二十三"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], "2017-07-23 00:00:00")

        test_str = "二零一七年七月二十三日"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], "2017-07-23 00:00:00")

        test_str = "二零一七年七月二十三日当天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], "2017-07-23 00:00:00")

        test_str = "2017年"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-01-01 00:00:00")
        self.assertEqual(d[1], "2017-12-31 00:00:00")

        test_str = "二零一七年"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-01-01 00:00:00")
        self.assertEqual(d[1], "2017-12-31 00:00:00")

        test_str = "2017年7月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-01 00:00:00")
        self.assertEqual(d[1], "2017-07-31 00:00:00")

        test_str = "二零一七年七月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-01 00:00:00")
        self.assertEqual(d[1], "2017-07-31 00:00:00")

        test_str = "7月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year}-07-01 00:00:00")
        self.assertEqual(d[1], f"{now.year}-07-31 00:00:00")

        test_str = "七月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year}-07-01 00:00:00")
        self.assertEqual(d[1], f"{now.year}-07-31 00:00:00")

        test_str = "07/11"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-07-11 00:00:00")

        test_str = "07-11"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-07-11 00:00:00")

        test_str = "07月11"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-07-11 00:00:00")

        test_str = "07月11日"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-07-11 00:00:00")

        test_str = "11日当天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-{v(now.month)}-11 00:00:00")

        test_str = "十一日当天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-{v(now.month)}-11 00:00:00")

        test_str = "今年"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year}-01-01 00:00:00")
        self.assertEqual(d[1], f"{now.year}-12-31 00:00:00")

        test_str = "明年"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year + 1}-01-01 00:00:00")
        self.assertEqual(d[1], f"{now.year + 1}-12-31 00:00:00")

        test_str = "去年"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year - 1}-01-01 00:00:00")
        self.assertEqual(d[1], f"{now.year - 1}-12-31 00:00:00")

        test_str = "前年"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year - 2}-01-01 00:00:00")
        self.assertEqual(d[1], f"{now.year - 2}-12-31 00:00:00")

        test_str = "本月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year}-{v(now.month)}-01 00:00:00")
        self.assertEqual(d[1], f"{month_last_day(now.year, now.month)} 00:00:00")

        test_str = "上月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year}-{v(now.month, -1)}-01 00:00:00")
        self.assertEqual(d[1], f"{month_last_day(now.year, now.month - 1)} 00:00:00")

        test_str = "下月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], f"{now.year}-{v(now.month, 1)}-01 00:00:00")
        self.assertEqual(d[1], f"{month_last_day(now.year, now.month + 1)} 00:00:00")

        test_str = "今天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-{v(now.month)}-{v(now.day)} 00:00:00")

        test_str = "明天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-{v(now.month)}-{v(now.day, 1)} 00:00:00")

        test_str = "后天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-{v(now.month)}-{v(now.day, 2)} 00:00:00")

        test_str = "昨天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-{v(now.month)}-{v(now.day, -1)} 00:00:00")

        test_str = "前天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-{v(now.month)}-{v(now.day, -2)} 00:00:00")


if __name__ == "__main__":
    unittest.main()
