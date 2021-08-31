import unittest
from nlp_date_spot import date_str_to_digit, parse_datetime
from datetime import date
from dateutil.relativedelta import relativedelta
import calendar


def start_of_month(date_time: date):
    return f"{date_time.year}-{str(date_time.month).rjust(2, '0')}-01 00:00:00"


def end_of_month(date_time: date):
    last = calendar.monthrange(date_time.year, date_time.month)[1]
    return f"{date_time.year}-{str(date_time.month).rjust(2, '0')}-{last} 00:00:00"


class NlpDateSpotTestCase(unittest.TestCase):
    def test_year_str_to_digit(self):
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

    def test_month_str_to_digit(self):
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

    def test_day_str_to_digit(self):
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

    def test_parse_year(self):
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

        test_str = "17年"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-01-01 00:00:00")
        self.assertEqual(d[1], "2017-12-31 00:00:00")

        test_str = "一七年"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-01-01 00:00:00")
        self.assertEqual(d[1], "2017-12-31 00:00:00")

    def test_parse_month(self):
        now = date.today()

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

    def test_parse_day(self):
        now = date.today()

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
        self.assertEqual(d[0], f"{now.year}-{str(now.month).rjust(2, '0')}-11 00:00:00")

        test_str = "十一日当天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], f"{now.year}-{str(now.month).rjust(2, '0')}-11 00:00:00")

    def test_parse_cn_date(self):
        now = date.today()

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
        self.assertEqual(d[0], start_of_month(now))
        self.assertEqual(d[1], end_of_month(now))

        test_str = "上月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], start_of_month(now - relativedelta(months=1)))
        self.assertEqual(d[1], end_of_month(now - relativedelta(months=1)))

        test_str = "下月"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], start_of_month(now + relativedelta(months=1)))
        self.assertEqual(d[1], end_of_month(now + relativedelta(months=1)))

        test_str = "今天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], now.strftime("%Y-%m-%d %H:%M:%S"))

        test_str = "明天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], (now + relativedelta(days=1)).strftime("%Y-%m-%d %H:%M:%S"))

        test_str = "后天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], (now + relativedelta(days=2)).strftime("%Y-%m-%d %H:%M:%S"))

        test_str = "昨天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], (now - relativedelta(days=1)).strftime("%Y-%m-%d %H:%M:%S"))

        test_str = "前天"
        d = parse_datetime(test_str)
        self.assertEqual(len(d), 1)
        self.assertEqual(d[0], (now - relativedelta(days=2)).strftime("%Y-%m-%d %H:%M:%S"))


if __name__ == "__main__":
    unittest.main()
