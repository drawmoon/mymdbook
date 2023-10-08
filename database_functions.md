## Convert

> 
> 
- Syntax

```sql
CONVERT(column::type)
```

- Arguments

| Name | Description |
| --- | --- |
| type |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
# PostgreSQL
now()::varchar

# MySQL
convert(now(), 'char')

# Sql Server
convert(varchar, getdate(), 121)

# Oracle
cast(systimestamp as varchar(50))
```

## Rpad

> 
> 
- Syntax

```sql
RPAD(text::integer::text)
```

- Arguments

| Name | Description |
| --- | --- |
| text |  |
| integer |  |
| text |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |

```sql
# PostgreSQL
rpad('2022', 8, '01')

# MySQL
rpad('2022', 8, '01')

# Sql Server
left('2022' + replicate('01', 4), 8)
```


## Now

> 
> 
- Syntax

```sql
NOW()
```

- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
# PostgreSQL
now()

# MySQL
now()

# Sql Server
getdate()

# Oracle
SYSTIMESTAMP
```
## Date

> 
> 
- Syntax

```sql
DATE(interval)
```

- Arguments

| Name | Description |
| --- | --- |
| interval |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
# PostgreSQL
date(now())

# MySQL
date(now())

# Sql Server
convert(date, getdate())

# Oracle
TRUNC(SYSTIMESTAMP, 'day')
```

## DateAdd

> 
> 
- Syntax

```sql
DATE_ADD(interval::'fmt')
```

- Arguments

| Name | Description |
| --- | --- |
| interval |  |
| fmt |  |
- Format

| Name | Description |
| --- | --- |
| Year |  |
| Quarter |  |
| Month |  |
| Week |  |
| Day |  |
| Hour |  |
| Minute |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
**# PostgreSQL**

# Year
now() + interval '1 year'
# Quarter
now() + interval '1 quarter'
# Month
now() + interval '1 month'
# Week
now() + interval '1 week'
# Day
now() + interval '1 day'
# Hour
now() + interval '1 hour'
# Minute
now() + interval '1 minute'
# Second
now() + interval '1 second'

**# MySQL**

# Year
date_add(now(), interval 1 year)
# Quarter
date_add(now(), interval 1 quarter)
# Month
date_add(now(), interval 1 month)
# Week
date_add(now(), interval 1 week)
# Day
date_add(now(), interval 1 day)
# Hour
date_add(now(), interval 1 hour)
# Minute
date_add(now(), interval 1 minute)
# Second
date_add(now(), interval 1 second)
```

## DateSub

> 
> 
- Syntax

```sql
DATE_SUB(interval::'fmt')
```

- Arguments

| Name | Description |
| --- | --- |
| interval |  |
| fmt |  |
- Format

| Name | Description |
| --- | --- |
| Year |  |
| Quarter |  |
| Month |  |
| Week |  |
| Day |  |
| Hour |  |
| Minute |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
**# PostgreSQL**

# Year
now() - interval '1 year'
# Quarter
now() - interval '1 quarter'
# Month
now() - interval '1 month'
# Week
now() - interval '1 week'
# Day
now() - interval '1 day'
# Hour
now() - interval '1 hour'
# Minute
now() - interval '1 minute'
# Second
now() - interval '1 second'

**# MySQL**

# Year
date_sub(now(), interval 1 year)
# Quarter
date_sub(now(), interval 1 quarter)
# Month
date_sub(now(), interval 1 month)
# Week
date_sub(now(), interval 1 week)
# Day
date_sub(now(), interval 1 day)
# Hour
date_sub(now(), interval 1 hour)
# Minute
date_sub(now(), interval 1 minute)
# Second
date_sub(now(), interval 1 second)
```

## Trunc

> 
> 
- Syntax

```sql
DATE_TRUNC(interval::'fmt')
```

- Arguments

| Name | Description |
| --- | --- |
| interval |  |
| fmt |  |
- Format

| Name | Description |
| --- | --- |
| Year | 从年份开始截断 |
| Quarter | 当前季度的第一天 |
| Month | 从月份开始截断 |
| Week | 当前周的第一天 |
| Day | 从日期开始截断 |
| Hour | 从小时开始截断 |
| Minute | 从分钟开始截断 |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
**# PostgreSQL**

# Year
date_trunc('year', now());
# Quarter
date_trunc('quarter', now());
# Month
date_trunc('month', now());
# Week
date_trunc('week', now());
# Day
date_trunc('day', now());
# Hour
date_trunc('hour', now());
# Minute
date_trunc('minute', now());

**# MySQL**

# Year
timestamp(date_format(now(), '%Y-01-01'))
# Quarter
timestamp(concat(year(now()), '-', ceil(month(now()) / 3) * 3 - 2, '-1'))
# Month
timestamp(date_format(now(), '%Y-%m-01'))
# Week
timestamp(date(date_sub(now(), interval weekday(now()) day)))
# Day
timestamp(date_format(now(), '%Y-%m-%d'))
# Hour
timestamp(date_format(now(), '%Y-%m-%d %H:00:00'))
# Minute
timestamp(date_format(now(), '%Y-%m-%d %H:%i:00'))

**# Sql Server**

# Year
convert(datetime, format(getdate(), 'yyyy-01-01'))
# Quarter
convert(datetime, concat(year(getdate()), '-', (ceiling(datepart(month, getdate()) / 3) + 1) * 3 - 2, '-1'))
# Month
convert(datetime, format(getdate(), 'yyyy-MM-01'))
# Week
convert(datetime, format(dateadd(day, -(datepart(weekday, getdate()) - 2), getdate()), 'yyyy-MM-dd'))
# Day
convert(datetime, format(getdate(), 'yyyy-MM-dd'))
# Hour
convert(datetime, format(getdate(), 'yyyy-MM-dd hh:00:00'))
# Minute
convert(datetime, format(getdate(), 'yyyy-MM-dd hh:mi:00'))

**# Oracle**

# Year
TRUNC(SYSTIMESTAMP, 'YEAR')
# Quarter
TRUNC(SYSTIMESTAMP, 'Q')
# Month
TRUNC(SYSTIMESTAMP, 'MONTH')
# Week
TRUNC(SYSTIMESTAMP, 'IW')
# Day
TRUNC(SYSTIMESTAMP, 'DD')
# Hour
TRUNC(SYSTIMESTAMP, 'HH')
# Minute
TRUNC(SYSTIMESTAMP, 'MI')
```

## DateFormat



## Extract

> 
> 
- Syntax

```sql
EXTRACT(interval::'fmt')
```

- Arguments

| Name | Description |
| --- | --- |
| interval |  |
| fmt |  |
- Format

| Name | Description |
| --- | --- |
| Year |  |
| Quarter |  |
| Month |  |
| Week |  |
| WeekDay |  |
| Day |  |
| Hour |  |
| Minute |  |
| Second |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
**# PostgreSQL**

# Year
extract(year from now())
# Quarter
extract(quarter from now())
# Month
extract(month from now())
# Week
extract(week from now())
# WeekDay
extract(dow from now())
# Day
extract(day from now())
# Hour
extract(hour from now())
# Minute
extract(minute from now())
# Second
extract(second from now())

**# MySQL**

# Year
year(now())
# Quarter
quarter(now())
# Month
month(now())
# Week
week(now())
# WeekDay
weekday(now())
# Day
day(now())
# Hour
hour(now())
# Minute
minute(now())
# Second
second(now())

**# Sql Server**

# Year
datepart(year, getdate())
# Quarter
datepart(quarter, getdate())
# Month
datepart(month, getdate())
# Week
datepart(week, getdate())
# WeekDay
datepart(weekday, getdate())
# Day
datepart(day, getdate())
# Hour
datepart(hour, getdate())
# Minute
datepart(minute, getdate())
# Second
datepart(second, getdate())

**# Oracle**

# Year
extract(year from systimestamp)
# Quarter
to_number(to_char(systimestamp, 'q'))
# Month
extract(month from systimestamp)
# Week
to_number(to_char(systimestamp, 'ww'))
# WeekDay
to_number(to_char(systimestamp, 'd'))
# Day
extract(day from systimestamp)
# Hour
extract(hour from systimestamp)
# Minute
extract(minute from systimestamp)
# Second
extract(second from systimestamp)
```

## Between

>
>
- Syntax

```sql
BETWEEB(expression::low::higt)
```
- Arguments

| Name | Description |
| --- | --- |
| statement |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
# BETWEEB(abc::3::10)
abc between 3 and 10
```

## RawSql

> RawSql 直通函数可用于將 SQL 运算表达式直接传送到数据库，而不由 U# 进行解析。
> 
- Syntax

```sql
RAW_SQL::statement
```

- Arguments

| Name | Description |
| --- | --- |
| statement |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
# RAW_SQL::date(now())
date(now())

# RAW_SQL::age::text
age::text
```

## Page

> 
> 
- Syntax

```sql
PAGE(statement::skip::limit)
```

- Arguments

| Name | Description |
| --- | --- |
| statement |  |
| skip |  |
| limit |  |
- Support

| Database | Support |
| --- | --- |
| PostgreSQL | ✓ |
| MySQL | ✓ |
| Sql Server | ✓ |
| Oracle | ✓ |

```sql
# PostgreSQL
from table1 limit 0 offset 5

# MySQL
from table1 limit 0,5

# Sql Server
from table1 order by id offset 0 rows fetch next 5 rows only

# Oracle
select "_t1".* from (select ROWNUM as "_", "_t".* from ((from table1) "_t")) "_t1" where rownum >= 0 and rownum <= 5

# Oracle (version major > 10)
from table1 offset 5 rows fetch first 10 rows only
```