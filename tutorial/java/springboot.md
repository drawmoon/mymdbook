# SpringBoot

## JavaBean

通过 `@Bean` 注解注入到容器：

```java
@Configuration
public class MyClass {
    @Bean
    public Bean1 bean1() {
        return new Bean1();
    }
}
```

通过 `@Resource` 注解实现属性注入：

```java
@Component
public class Application {
    @Resource
    private Bean1 bean1;
}
```

> 推荐使用 `@Resource` 而不是 `@Autowired`，`@Resource` 注解属于 J2EE 的，减少了与 Spring 的耦合。

### 注册多个 JavaBean 到容器

通过 `@Qualifier` 指定注入的 Bean，或通过 `@Primary` 指定默认注入的 Bean：

```java
@Configuration
public class MyClass {
    @Bean
    @Primary
    public Bean1 bean1() {
        return new Bean1();
    }

    @Bean("bean2")
    public Bean1 bean2() {
        return new Bean1();
    }
}
```

属性注入：

```java
@Component
public class Application {
    @Resource
    @Qualifier("bean2")
    private Bean1 bean1;
}
```

### 在单元测试中注册 JavaBean

```java
@RunWith(SpringRunner.class)
@SpringBootTest
@SpringBootConfiguration
public class ApplicationTests {
    @Bean
    public Bean1 bean1() {
        return new Bean1();
    }

    @Test
    public void test() {
        // 在上面已经将 Bean1 对象注册到了 Spring 容器中，SystemService 内部可以正常使用 Bean1 对象。
        SystemService systemService = new SystemService();
        systemService.get();
    }
}

@Component
public class SystemService {
    @Resource
    private Bean1 bean1;

    public void get() {
        System.out.println(bean1);
    }
}
```

如果 `SystemService` 使用 Hutool 的工具类 `SpringUtil.getBean` 动态获取 JavaBean 的方法，需要手动注册 `SpringUtil`：

```java
@RunWith(SpringRunner.class)
@SpringBootTest
@SpringBootConfiguration
@ComponentScan(basePackages={"cn.hutool.extra.spring"})
public class ApplicationTests {
    @Bean
    public Bean1 bean1() {
        return new Bean1();
    }

    @Test
    public void test() {
        // 在上面已经将 Bean1 对象注册到了 Spring 容器中，SystemService 内部可以正常使用 Bean1 对象。
        SystemService systemService = new SystemService();
        systemService.get();
    }
}

@Component
public class SystemService {
    public void get() {
        Bean1 bean1 = SpringUtil.getBean(Bean1.class);
        System.out.println(bean1);
    }
}
```

## 程序启动成功后执行

```java
@Component
public class MyAppCommandLineRunner implements CommandLineRunner {
    @Override
    public void run(String... args) {
        // 在这里编写你的程序启动后要执行的逻辑...
    }
}
```
