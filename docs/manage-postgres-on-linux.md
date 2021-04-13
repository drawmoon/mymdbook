# Linux 下管理 Postgres

## 修改 Postgres 密码

```bash
# 切换用户到 postgres
su - postgres

# 进入到 bin 目录下
psql -U postgres

# 修改密码
$ postgres=# alter user postgres with password 'postgres';

# 退出
$ postgres=# \q
```

## 创建 Postgres 用户和数据库

```bash
# 切换用户到 postgres
su - postgres

# 进入到 bin 目录下
psql -U postgres

# 创建用户
$ postgres=# create user ioea with password 'ioea';

# 创建用户数据库
$ postgres=# create database ioea owner ioeadb;

# 将 ioeadb 数据库的所有权限都赋予 ioea 用户
$ postgres=# grant all privileges on database ioeadb to ioea;

# 退出
$ postgres=# \q

# 创建 linux 用户
sudo adduser ioea
sudo passwd ioea

# 切换用户到 ioea
su - ioea
psql -d ioeadb
revoke connect on database ioeadb from public;
```
