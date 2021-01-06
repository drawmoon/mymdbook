# Oracle 查询列信息

```sql
select t1.TABLE_NAME,
       t1.COLUMN_NAME,
       t1.DATA_TYPE,
       t1.DATA_LENGTH,
       t1.NULLABLE,
       t1.DATA_PRECISION,
       t2.COMMENTS,
       CASE t1.COLUMN_NAME
            WHEN (select col.COLUMN_NAME
                from user_constraints con,
                   user_cons_columns col
                where con.constraint_name = col.constraint_name
                  and con.constraint_type = 'P'
                  and col.TABLE_NAME = t1.TABLE_NAME
                  and col.COLUMN_NAME = t1.COLUMN_NAME) THEN 'Y'
           ELSE 'N'
           END as primary_key
from all_tab_columns t1
        inner join all_col_comments t2 on t1.TABLE_NAME = t2.TABLE_NAME and t1.COLUMN_NAME = t2.COLUMN_NAME
where t1.OWNER = '${schema}'
    and t1.TABLE_NAME = '${table}'
    and t1.COLUMN_NAME = '${column}'
```
