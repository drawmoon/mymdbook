import unittest
from date_parser import str_to_digit, parse, merge


class DateParserTestCase(unittest.TestCase):
    def test_merge(self):
        ner = [("2017年", "DATE", 0, 1), ("2021年", "DATE", 3, 4)]
        rst = merge(ner)
        self.assertEqual(len(rst), 2)
        self.assertEqual(rst[0], ("2017年", "DATE", 0, 1))
        self.assertEqual(rst[1], ("2021年", "DATE", 3, 4))

        ner = [("第四", "ORDINAL", 0, 1), ("季度", "DATE", 1, 2)]
        rst = merge(ner)
        self.assertEqual(len(rst), 1)
        self.assertEqual(rst[0], ("第四季度", "DATE", 0, 2))

        ner = [("2021年7月28日", "DATE", 4, 7), ("当天", "DATE", 7, 8)]
        rst = merge(ner)
        self.assertEqual(len(rst), 1)
        self.assertEqual(rst[0], ("2021年7月28日当天", "DATE", 4, 8))

    def test_year_str_to_digit(self):
        test_str = "17"
        d = str_to_digit(test_str, "year")
        self.assertEqual(d, 2017)

        test_str = "2017"
        d = str_to_digit(test_str, "year")
        self.assertEqual(d, 2017)

        test_str = "一七"
        d = str_to_digit(test_str, "year")
        self.assertEqual(d, 2017)

        test_str = "二零一七"
        d = str_to_digit(test_str, "year")
        self.assertEqual(d, 2017)

        test_str = "07"
        d = str_to_digit(test_str, "year")
        self.assertEqual(d, 2007)

        test_str = "零七"
        d = str_to_digit(test_str, "year")
        self.assertEqual(d, 2007)

    def test_month_str_to_digit(self):
        test_str = "12"
        d = str_to_digit(test_str, "month")
        self.assertEqual(d, 12)

        test_str = "十二"
        d = str_to_digit(test_str, "month")
        self.assertEqual(d, 12)

        test_str = "1"
        d = str_to_digit(test_str, "month")
        self.assertEqual(d, 1)

        test_str = "一"
        d = str_to_digit(test_str, "month")
        self.assertEqual(d, 1)

    def test_day_str_to_digit(self):
        test_str = "31"
        d = str_to_digit(test_str, "day")
        self.assertEqual(d, 31)

        test_str = "三十一"
        d = str_to_digit(test_str, "day")
        self.assertEqual(d, 31)

        test_str = "1"
        d = str_to_digit(test_str, "day")
        self.assertEqual(d, 1)

        test_str = "一"
        d = str_to_digit(test_str, "day")
        self.assertEqual(d, 1)

    def test_parse_year(self):
        test_str = "2017年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-01-01 00:00:00")
        self.assertEqual(d[1], "2018-01-01 00:00:00")

        test_str = "二零一七年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-01-01 00:00:00")
        self.assertEqual(d[1], "2018-01-01 00:00:00")

        test_str = "17年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-01-01 00:00:00")
        self.assertEqual(d[1], "2018-01-01 00:00:00")

        test_str = "一七年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-01-01 00:00:00")
        self.assertEqual(d[1], "2018-01-01 00:00:00")

    def test_parse_month(self):
        test_str = "2017年7月"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-01 00:00:00")
        self.assertEqual(d[1], "2017-08-01 00:00:00")

        test_str = "二零一七年七月"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-01 00:00:00")
        self.assertEqual(d[1], "2017-08-01 00:00:00")

        test_str = "7月"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-07-01 00:00:00")
        self.assertEqual(d[1], "2021-08-01 00:00:00")

        test_str = "七月"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-07-01 00:00:00")
        self.assertEqual(d[1], "2021-08-01 00:00:00")

    def test_parse_day(self):
        test_str = "2017/7/23"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-23 00:00:00")
        self.assertEqual(d[1], "2017-07-24 00:00:00")

        test_str = "2017-7-23"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-23 00:00:00")
        self.assertEqual(d[1], "2017-07-24 00:00:00")

        test_str = "2017年7月23日"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-23 00:00:00")
        self.assertEqual(d[1], "2017-07-24 00:00:00")

        test_str = "二零一七年七月二十三"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-23 00:00:00")
        self.assertEqual(d[1], "2017-07-24 00:00:00")

        test_str = "二零一七年七月二十三日"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-23 00:00:00")
        self.assertEqual(d[1], "2017-07-24 00:00:00")

        test_str = "二零一七年七月二十三日当天"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2017-07-23 00:00:00")
        self.assertEqual(d[1], "2017-07-24 00:00:00")

        test_str = "07/11"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-07-11 00:00:00")
        self.assertEqual(d[1], "2021-07-12 00:00:00")

        test_str = "07-11"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-07-11 00:00:00")
        self.assertEqual(d[1], "2021-07-12 00:00:00")

        test_str = "07月11"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-07-11 00:00:00")
        self.assertEqual(d[1], "2021-07-12 00:00:00")

        test_str = "07月11日"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-07-11 00:00:00")
        self.assertEqual(d[1], "2021-07-12 00:00:00")

        test_str = "11日当天"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-11 00:00:00")
        self.assertEqual(d[1], "2021-09-12 00:00:00")

        test_str = "十一日当天"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-11 00:00:00")
        self.assertEqual(d[1], "2021-09-12 00:00:00")

    def test_parse_cn_date(self):
        test_str = "今年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-01-01 00:00:00")
        self.assertEqual(d[1], "2022-01-01 00:00:00")

        test_str = "明年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2022-01-01 00:00:00")
        self.assertEqual(d[1], "2023-01-01 00:00:00")

        test_str = "去年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2020-01-01 00:00:00")
        self.assertEqual(d[1], "2021-01-01 00:00:00")

        test_str = "前年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2019-01-01 00:00:00")
        self.assertEqual(d[1], "2020-01-01 00:00:00")

        test_str = "本月"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-01 00:00:00")
        self.assertEqual(d[1], "2021-10-01 00:00:00")

        test_str = "上月"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-08-01 00:00:00")
        self.assertEqual(d[1], "2021-09-01 00:00:00")

        test_str = "下月"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-10-01 00:00:00")
        self.assertEqual(d[1], "2021-11-01 00:00:00")

        test_str = "今天"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-01 00:00:00")
        self.assertEqual(d[1], "2021-09-02 00:00:00")

        test_str = "明天"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-02 00:00:00")
        self.assertEqual(d[1], "2021-09-03 00:00:00")

        test_str = "后天"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-03 00:00:00")
        self.assertEqual(d[1], "2021-09-04 00:00:00")

        test_str = "昨天"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-08-31 00:00:00")
        self.assertEqual(d[1], "2021-09-01 00:00:00")

        test_str = "前天"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-08-30 00:00:00")
        self.assertEqual(d[1], "2021-08-31 00:00:00")

        test_str = "上午"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-01 00:00:00")
        self.assertEqual(d[1], "2021-09-01 12:00:00")

        test_str = "下午"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-01 12:00:00")
        self.assertEqual(d[1], "2021-09-01 19:00:00")

        test_str = "本周"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-08-30 00:00:00")
        self.assertEqual(d[1], "2021-09-06 00:00:00")

        test_str = "上周"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-08-23 00:00:00")
        self.assertEqual(d[1], "2021-08-30 00:00:00")

        test_str = "下周"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-09-06 00:00:00")
        self.assertEqual(d[1], "2021-09-13 00:00:00")

        test_str = "上半年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-01-01 00:00:00")
        self.assertEqual(d[1], "2021-07-01 00:00:00")

        test_str = "下半年"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-07-01 00:00:00")
        self.assertEqual(d[1], "2022-01-01 00:00:00")

        test_str = "第一季度"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-01-01 00:00:00")
        self.assertEqual(d[1], "2021-04-01 00:00:00")

        test_str = "第二季度"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-04-01 00:00:00")
        self.assertEqual(d[1], "2021-07-01 00:00:00")

        test_str = "第三季度"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-07-01 00:00:00")
        self.assertEqual(d[1], "2021-10-01 00:00:00")

        test_str = "第四季度"
        d = parse(test_str)
        self.assertEqual(len(d), 2)
        self.assertEqual(d[0], "2021-10-01 00:00:00")
        self.assertEqual(d[1], "2022-01-01 00:00:00")


if __name__ == "__main__":
    unittest.main()
