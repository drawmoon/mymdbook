# SpringBoot

## 依赖注入

通过 `@Bean` 注解注入到容器：

```java
@Bean
public Bean1 bean1() {
    return new Bean1();
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

### 多个 Bean 实例

通过 `@Qualifier` 指定注入的 Bean，或通过 `@Primary` 指定默认注入的 Bean：

```java
@Bean
@Primary
public Bean1 bean1() {
    return new Bean1();
}

@Bean("bean2")
public Bean1 bean2() {
    return new Bean1();
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
