import unittest
from nlp_date_spot import date_str_to_digit, parse_datetime
from datetime import date


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
        test_str = "2017/7/23"
        d = parse_datetime(test_str)
        self.assertEqual(d, "2017-07-23 00:00:00")

        test_str = "2017-7-23"
        d = parse_datetime(test_str)
        self.assertEqual(d, "2017-07-23 00:00:00")

        test_str = "2017年7月23日"
        d = parse_datetime(test_str)
        self.assertEqual(d, "2017-07-23 00:00:00")

        test_str = "二零一七年七月二十三日当天"
        d = parse_datetime(test_str)
        self.assertEqual(d, "2017-07-23 00:00:00")

        test_str = "二零一七年七月二十三日"
        d = parse_datetime(test_str)
        self.assertEqual(d, "2017-07-23 00:00:00")

        test_str = "07-11"
        d = parse_datetime(test_str)
        self.assertEqual(d, "2021-07-11 00:00:00")

        test_str = "十一日当天"
        d = parse_datetime(test_str)
        self.assertEqual(d, "2021-08-11 00:00:00")

        test_str = "今天"
        d = parse_datetime(test_str)
        self.assertEqual(d, f"{date.today()} 00:00:00")


if __name__ == "__main__":
    unittest.main()
