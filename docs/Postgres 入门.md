# Postgres 入门

- [Linux 下管理 Postgres](#linux-下管理-postgres)
- [年季月周日条件过滤](#年季月周日条件过滤)

## Linux 下管理 Postgres

### 修改密码

切换用户到 `postgres`

```bash
su - postgres
```

登录数据库

```bash
psql -U postgres
```

修改密码

```bash
$ postgres=# alter user postgres with password 'postgres';
```

退出

```bash
$ postgres=# \q
```

### 新建用户与数据库

切换为 `postgres` 用户

```bash
su - postgres
```

登录数据库

```bash
psql -U postgres
```

创建用户 `ioea`

```bash
$ postgres=# create user ioea with password 'ioea';
```

创建数据库 `ioeadb`，所有者为 `ioea` 用户

```bash
$ postgres=# create database ioea owner ioeadb;
```

将数据库 `ioeadb` 的所有权限都赋予 `ioea` 用户

```bash
$ postgres=# grant all privileges on database ioeadb to ioea;
```

退出

```bash
$ postgres=# \q
```

创建用户

```bash
sudo adduser ioea
sudo passwd ioea
```

登录数据库

```bash
su - ioea
psql -d ioeadb
```

设置数据库 `ioeadb` 的连接权限为所有用户

```bash
revoke connect on database ioeadb from public;
```

## 年季月周日条件过滤

### 年

```sql
-- 本年
to_char(<COLUMN>, 'yyyy') = to_char(now(), 'yyyy')

-- 去年
to_char(<COLUMN>, 'yyyy') = to_char(now() - interval '1' year, 'yyyy')

-- 前年
to_char(<COLUMN>, 'yyyy') = to_char(now() - interval '2' year, 'yyyy')

-- 3年前（不包含当年）
to_char(<COLUMN>, 'yyyy') >= to_char(now() - interval '3' year, 'yyyy') and to_char(<COLUMN>, 'yyyy') < to_char(now(), 'yyyy')

-- 3年之内（包含当年）
to_char(<COLUMN>, 'yyyy') >= to_char(now() - interval '2' year, 'yyyy')
```

### 季度

```sql
-- 本季度
to_char(<COLUMN>, 'yyyy-Q') = to_char(now(), 'yyyy-Q')

-- 上季度
to_char(<COLUMN>, 'yyyy-Q') = to_char(now() - interval '3' month, 'yyyy-Q')

-- 3季度前（不包含当季度）
to_char(<COLUMN>, 'yyyy-Q') >= to_char(now() - interval '9' month, 'yyyy-Q') and to_char(<COLUMN>, 'yyyy-Q') < to_char(now(), 'yyyy-Q')

-- 3季度之内（包含当季度）
to_char(<COLUMN>, 'yyyy-Q') >= to_char(now() - interval '6' month, 'yyyy-Q')
```

### 月

```sql
-- 本月
to_char(<COLUMN>, 'yyyy-MM') = to_char(now(), 'yyyy-MM')

-- 上月
to_char(<COLUMN>, 'yyyy-MM') = to_char(now() - interval '1' month, 'yyyy-MM')

-- 3月前（不包含当月）
to_char(<COLUMN>, 'yyyy-MM') >= to_char(now() - interval '3' month, 'yyyy-MM') and to_char(<COLUMN>, 'yyyy-MM') < to_char(now(), 'yyyy-MM')

-- 3月之内（包含当月）
to_char(<COLUMN>, 'yyyy-MM') >= to_char(now() - interval '2' month, 'yyyy-MM')
```

### 日

```sql
-- 今天
to_char(<COLUMN>, 'yyyy-MM-dd') = to_char(now(), 'yyyy-MM-dd')

-- 昨天
to_char(<COLUMN>, 'yyyy-MM-dd') = to_char(now() - interval '1' day, 'yyyy-MM-dd')

-- 近7天
to_char(<COLUMN>, 'yyyy-MM-dd') >= to_char(now() - interval '6' day, 'yyyy-MM-dd')

-- 近30天
to_char(<COLUMN>, 'yyyy-MM-dd') >= to_char(now() - interval '29' day, 'yyyy-MM-dd')

-- 10天前（不包含当天）
to_char(<COLUMN>, 'yyyy-MM-dd') >= to_char(now() - interval '10' day, 'yyyy-MM-dd') and to_char(<COLUMN>, 'yyyy-MM-dd') < to_char(now(), 'yyyy-MM-dd')

-- 10天之内（包含当天）
to_char(<COLUMN>, 'yyyy-MM-dd') >= to_char(now() - interval '9' day, 'yyyy-MM-dd')
```

### 时

```sql
-- 近24小时
<COLUMN> >= now() - interval '23' hour;
```
