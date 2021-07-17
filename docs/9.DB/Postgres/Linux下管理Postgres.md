# Linux 下管理 Postgres

## 修改密码

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

## 新建用户与数据库

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
