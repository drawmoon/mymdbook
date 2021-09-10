from datetime import datetime, date, timedelta
from dateutil.relativedelta import relativedelta
from dateutil.parser import parse
import calendar

# 获取当前时间
now = datetime.now()
print(now)

# 获取当前时间，不包含时分秒
today = date.today()
print(today)

year = date.today().year
quarter = (date.today().month - 1) // 3 + 1
month = date.today().strftime("%Y-%m")
day = date.today()
print("本年", year, "本季度", quarter, "本月", month, "本日", day)

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

last_quarter = now - relativedelta(months=3)
print("上季度", (last_quarter.month - 1) // 3 + 1)

next_quarter = now + relativedelta(months=3)
print("下季度", (next_quarter.month - 1) // 3 + 1)

print("本月最后一天", calendar.monthrange(now.year, now.month))

print("本周第一天", now - timedelta(days=now.weekday()))
print("本周最后一天", now + timedelta(days=6 - now.weekday()))

last_week = now - timedelta(weeks=1)
print("上周第一天", last_week - timedelta(days=last_week.weekday()))

next_week = now + timedelta(weeks=1)
print("下周第一天", next_week - timedelta(days=next_week.weekday()))

print(datetime(2020, 1, 1).strftime("%Y-%m-%d %H:%M:%S"))
print(datetime(2020, 1, 1, 23, 59, 59).strftime("%Y-%m-%d %H:%M:%S"))
