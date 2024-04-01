# MyBatis

## XML 文件多数据库供应商支持

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

## 根据目标数据库类型动态调整模型列名

```java
import java.lang.reflect.Field;
import java.lang.reflect.Proxy;
import java.util.Arrays;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Optional;

import javax.persistence.Column;
import javax.persistence.Table;

import org.springframework.beans.BeansException;
import org.springframework.beans.factory.config.BeanDefinition;
import org.springframework.beans.factory.config.BeanFactoryPostProcessor;
import org.springframework.beans.factory.config.ConfigurableListableBeanFactory;
import org.springframework.boot.context.properties.bind.Binder;
import org.springframework.context.EnvironmentAware;
import org.springframework.context.annotation.ClassPathScanningCandidateComponentProvider;
import org.springframework.core.env.Environment;
import org.springframework.core.type.filter.AnnotationTypeFilter;
import org.springframework.stereotype.Component;
import org.springframework.web.context.support.StandardServletEnvironment;

@Component
public class DatabaseColumnStyleRender implements BeanFactoryPostProcessor, EnvironmentAware {

    private Environment environment;

    public DbDialect getDbDialect() {
        String sqlDialect = Binder.get(environment).bind("sql.dialect", String.class).get();
        return DbDialect.valueOfIgnoreCase(sqlDialect);
    }

    @Override
    public void setEnvironment(Environment environment) {
        this.environment = environment;
    }

    @Override
    public void postProcessBeanFactory(ConfigurableListableBeanFactory beanFactory) throws BeansException {
        DbDialect dbDialect = this.getDbDialect();

        if (dbDialect != null) {
            String[] packages = new String[] {"com.example.model"};
            this.changeColumnAnnotationName(packages, dbDialect);
        }
    }

    private void changeColumnAnnotationName(String[] packageNames, DbDialect dbDialect) {
        List<Class<?>> classes = this.findModelClassesInPackage(packageNames);
        for (Class<?> clazz : classes) {
            Field[] modelFields = clazz.getDeclaredFields();
            for (Field modelField : modelFields) {
                Column column = modelField.getAnnotation(Column.class);
                if (column == null) {
                    continue;
                }

                Object handler = Proxy.getInvocationHandler(column);
                try {
                    Field field = handler.getClass().getDeclaredField("memberValues");
                    field.setAccessible(true);

                    Object valuePair = field.get(handler);
                    @SuppressWarnings("unchecked")
                    Map<String, Object> members = (LinkedHashMap<String, Object>) valuePair;
                    Object raw = members.get("name");
                    if (raw == null && !(raw instanceof String)) {
                        continue;
                    }
                    members.put("name", dbDialect.renderColumnName((String) raw));
                } catch (Exception e) {
                    throw new RuntimeException(e);
                }
            }
        }
    }

    private List<Class<?>> findModelClassesInPackage(String[] packageNames) {
        List<Class<?>> modelClasses = new LinkedList<Class<?>>();

        for (String pkg : packageNames) {
            Environment env = new StandardServletEnvironment();
            ClassPathScanningCandidateComponentProvider p = new ClassPathScanningCandidateComponentProvider(true, env);
            p.addIncludeFilter(new AnnotationTypeFilter(Table.class));

            for (BeanDefinition def : p.findCandidateComponents(pkg)) {
                try {
                    modelClasses.add(Class.forName(def.getBeanClassName()));
                } catch (ClassNotFoundException e) {
                    // ignore
                }
            }
        }

        return modelClasses;
    }

    @Getter
    public static enum DbDialect {

        POSTGRESQL("\"", false),
        MYSQL("`", false),
        ORACLE("\"", true)
        ;

        private String delimiter;

        private boolean columnNameUpperCase;

        DbDialect(String delimiter, boolean columnNameUpperCase) {
            this.delimiter = delimiter;
            this.columnNameUpperCase = columnNameUpperCase;
        }

        public String renderColumnName(String name) {
            if (this.columnNameUpperCase) {
                name = name.toUpperCase();
            }
            return delimiter + name + delimiter;
        }

        public static DbDialect valueOfIgnoreCase(String value) {
            Optional<DbDialect> dialectOptional = Arrays.stream(values()).filter(x -> x.name().equalsIgnoreCase(value)).findFirst();
            if (dialectOptional.isPresent()) {
                return dialectOptional.get();
            }
            return null;
        }
    }

}
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
