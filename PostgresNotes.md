# Postgres 查询列信息

```sql
select col.column_name,
       col.data_type,
       col.dtd_identifier,
       col.numeric_scale,
       col.numeric_precision,
       col.is_nullable,
       col.table_name,
       col.ordinal_position,
       col.character_maximum_length,
       col.numeric_precision_radix,
       col.datetime_precision,
       des.description,
       kcu.constraint_type = 'PRIMARY KEY' as primary_key
from information_schema.columns col
         left join (select distinct des.objsubid, cls.relname, des.description
                    from pg_description des
                             inner join pg_class cls on des.objoid = cls.oid) des
                   on des.relname = col.table_name and col.ordinal_position = des.objsubid
         left join (select distinct tco.constraint_type, tco.table_schema, tco.table_name, kcu.column_name
                    from information_schema.key_column_usage kcu
                             left join information_schema.table_constraints tco
                                       on kcu.constraint_name = tco.constraint_name and
                                          kcu.constraint_schema = tco.constraint_schema) kcu
                   on (kcu.table_schema = col.table_schema and kcu.table_name = col.table_name and
                       kcu.column_name = col.column_name)
where col.table_schema = '${schema}'
  and col.table_name = '${table}'
  and col.column_name = '${column}';

```
