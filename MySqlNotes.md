# MySql 查询列信息

```sql
select col.table_schema,
       col.column_name,
       col.data_type,
       col.numeric_scale,
       col.numeric_precision,
       col.is_nullable,
       col.table_name,
       col.ordinal_position,
       col.column_comment,
       col.character_maximum_length,
       col.column_type,
       sta.INDEX_NAME = 'primary' and sta.INDEX_NAME is not null as primary_key
from information_schema.columns col
         left join (select distinct INDEX_NAME, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME
                    from information_schema.statistics) sta
                   on (sta.TABLE_SCHEMA = col.TABLE_SCHEMA and sta.TABLE_NAME = col.TABLE_NAME and
                       sta.COLUMN_NAME = col.COLUMN_NAME)
where col.TABLE_NAME = '${table}'
  and col.TABLE_SCHEMA = '${schema}'
  and column_name = '${column}'

```
