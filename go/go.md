# Go

## 基本

### 函数的声明与调用

匿名返回值

```go
func add(x, y int) (s int) {
    s = x + y
    return
}
```

### 结构体 struct

```go
type User struct {
    id string
    firstName string
    lastName string
    age int
}
```

构建结构体

```go
var user = User{ "1", "d", "rsh", 25 }
```

### 结构体的函数 func

给 `User` 结构体声明 `FullName` 的方法

```go
type User struct {
    id string
    firstName string
    lastName string
    age int
}

func (user User) FullName() string {
    return user.firstName + user.lastName
}
```

#### 属主参数的传参是一个值复制过程

在下列例子中，`SetPageSize` 方法不会改变属主的 `pageSize` 字段的值

```go
type Page struct {
    page int
    pageSize int
}

func (p Page) SetPageSize(pageSize int) {
    p.pageSize = pageSize
}
```

如果希望改变属主字段的值，使用指针 `*T` 将参数指向同一块内存

```go
func (p *Page) SetPageSize(pageSize int) {
    p.pageSize = pageSize
}
```

### 接口 interface

在面向对象编程中，接口定义了对象的行为，而具体的实现行为则取决于对象。在 Go 中，实现的关系是隐式的，接口是一组函数签名，当一个类型为接口中的所有函数提供定义时，它被称为实现接口。Go 中没有 `implements` 关键字。Go 编译器会自动检查两个类型之间的实现关系

```go
type IUser interface {
    FullName() string
}

type User struct {
    id string
    firstName string
    lastName string
    age int
}

func (user User) FullName() string {
    return user.firstName + user.lastName
}
```

### 范型

```go
type User[TKey string | int] {
    id TKey
}

func CreateUser[TKey string | int] User[TKey] {
    // Method implement.
}
```

### 正则表达式 regexp

匹配括号中（子表达式）的文本

```go
const (
    regex = "^([f|d])(\\d+)$"
    str = "f1"
)

p := regexp.MustCompile(regex)
g := p.FindStringSubmatch(str)

// Output:
// ["f1", "f", "1"]
```

### Goroutine

通过通道（channel）来实现并行执行：

```go
func main() {
    ch := make(chan int)

    for i := 1; i <= 5; i++ {
        go func(id int, ch chan<- int) {
            time.Sleep(time.Second)
            ch <- id
        }(i, ch)
    }

    for i := 1; i <= 5; i++ {
        result := <-ch
        fmt.Println("Result:", result)
    }
}
```

通过 `sync.WaitGroup` 来实现并发和同步：

```go
func main() {
    var wg sync.WaitGroup

    for i := 1; i <= 5; i++ {
        wg.Add(1)
        go func(id int) {
            defer wg.Done()
            time.Sleep(time.Second)
        }(i)
    }

    wg.Wait()

    fmt.Println("All workers done")
}
```
