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
month = date.today().strftime("%Y-%m")
day = date.today()
print("本年", year, "本月", month, "本日", day)

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

# 本月最后一天
print(calendar.monthrange(now.year, now.month))

# 本周第一天
print(now - timedelta(days=now.weekday()))
# 本周最后一天
print(now + timedelta(days=6 - now.weekday()))
