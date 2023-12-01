# Postgres

## 递归查询

```sql
create table directory(id int not null,name text not null,parent_id int);
insert into directory (id, name, parent_id) values (1, '新建目录1', null);
insert into directory (id, name, parent_id) values (2, '新建目录1的子目录1', 1);
insert into directory (id, name, parent_id) values (3, '新建目录1的子目录1的子目录1', 2);
insert into directory (id, name, parent_id) values (4, '新建目录2', null);
select * from directory;
```

| id | name | parent_id |
| --- | --- | --- |
| 1 | 新建目录1 | |
| 2 | 新建目录1的子目录1 |1 |
| 3 | 新建目录1的子目录1的子目录1 | 2 |
| 4 | 新建目录2 | |

```sql
WITH RECURSIVE children AS (
    SELECT id,parent_id
       FROM directory
    WHERE parent_id in (1)
    union ALL
    SELECT directory.id,directory.parent_id
    FROM directory,children
    WHERE directory.parent_id = children.id
)
select * from directory where id in (SELECT id FROM children);
```

| id | name | parent_id |
| --- | --- | --- |
| 2 | 新建目录1的子目录1 |1 |
| 3 | 新建目录1的子目录1的子目录1 | 2 |
