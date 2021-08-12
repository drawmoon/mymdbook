import datetime

# 获取当前时间
today = datetime.datetime.now()
print(today)

# 获取当前时间，不包含时分秒
today = datetime.date.today()
print(today)

# 加一天
print(today + datetime.timedelta(days=1))

# 减一天
print(today - datetime.timedelta(days=1))
