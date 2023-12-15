import calendar

from datetime import datetime, date, timedelta
from dateutil.relativedelta import relativedelta


# 获取当前时间
now = datetime.now()
print(now)


# 获取当前时间，不包含时分秒
today = date.today()
print(today)

year = date.today().year
quarter = (date.today().month - 1) // 3 + 1
month = date.today().month
day = date.today().day
print("本年", year, "本季度", quarter, "本月", month, "本日", day)


# 获取时间的最小值
print(datetime.min, date.min)
# 获取时间的最大值
print(datetime.max, date.max)


# 加一天
print(now + timedelta(days=1))


# 减一天
print(now - timedelta(days=1))
print(now + timedelta(days=-1))


# 加一月
print(now + relativedelta(months=1))


# 减一月
print(now - relativedelta(months=1))
print(now + relativedelta(months=-1))


# 加一年
print(now + relativedelta(years=1))


# 减一年
print(now - relativedelta(years=1))
print(now + relativedelta(years=-1))


# 上季度
last_quarter = now - relativedelta(months=3)
print((last_quarter.month - 1) // 3 + 1)


# 下季度
next_quarter = now + relativedelta(months=3)
print((next_quarter.month - 1) // 3 + 1)


# 本月最后一天
print(calendar.monthrange(now.year, now.month))


# 本周第一天 与 本周最后一天
print(now - timedelta(days=now.weekday()))
print(now + timedelta(days=6 - now.weekday()))


# 上周第一天 与 下周第一天
last_week = now - timedelta(weeks=1)
print(last_week - timedelta(days=last_week.weekday()))
next_week = now + timedelta(weeks=1)
print(next_week - timedelta(days=next_week.weekday()))


# 日期格式化
print(datetime.now().strftime("%Y-%m-%d %H:%M:%S"))
print(date.today().strftime("%Y-%m-%d %H:%M:%S"))


# datetime 转 date
now = datetime.now()
print(now.date())
