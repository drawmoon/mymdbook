# MyBatis

## 动态获取 Mapper

```java
import cn.hutool.extra.spring.SpringUtil;
import org.springframework.core.ResolvableType;

var c = Directory.class;
var type = ResolvableType.forClassWithGenerics(Mapper.class, c);
var names = SpringUtil.getApplicationContext().getBeanNamesForType(type);
return SpringUtil.getBean(names[0]);
```

## 嵌套结果 Nested Results

```sql
create table order(id integer primary key, name varchar(100));
create table order_detail(id integer primary key, name varchar(100), order_id integer);
```

```java
@Table
public class Order {
  @Id
  private Integer id;
  @Column
  private String name;
  @Transient
  private List<OrderDetail> details;
}

@Table
public class OrderDetail {
  @Id
  private Integer id;
  @Column
  private String name;
  @Column
  private Integer orderId;
  @Transient
  private Order order;
}
```

### 属性 Association

### 集合 Collection

```xml
<resultMap id="includeDetailMap" type="com.osy.model.Order">
  <id property="id" column="id" />
  <result property="name" column="name" />
  <collection property="details" columnPrefix="details_" javaType="ArrayList" ofType="com.osy.model.OrderDetail">
    <id property="id" column="id" />
    <result property="name" column="name" />
    <result property="orderId" column="order_id" />
  </collection>
</resultMap>
```

```xml
<select id="selectIncludeDetailById" resultMap="includeDetailMap">
    select
    o.id, o.name, od.id as details_id,
    od.name as details_name od.order_id as details_order_id
 from
 order o
 inner join (
 select id, name
 from
 order_detail) od
 on
 ff.id = ffs.filtration_id
    where
    ff.id = #{id};
</select>
```

### 多数据库供应商支持

```xml
<select id="selectAll">
  select
  <if test="_databaseId == 'mysql'">
    o.`name`
  </if>
  <if test="_databaseId == 'postgres'">
    o."name"
  </if>
  from
  order o
</select>
```
