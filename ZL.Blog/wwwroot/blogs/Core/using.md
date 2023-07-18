## Using声明（C#8.0）
在实现了IDisposable/IAsyncDisposable接口的类型的变量声明前加上using，当代码执行离开变量的作用域时，对象就会被释放。
```c#
using SqlConnection conn = new SqlConnection("...");
```

## 顶级语句（C#9.0）
1. 直接在C#文件中编写入口方法的代码，不用类，不用Main。经典写法仍然支持。
2. 同一个项目中只能有一个文件具有顶级语句。
3. 顶级语句中可以直接使用await语法，也可以申明函数。

## 全局Using指令（C#10）
1. 将global修饰符添加到using前，这个命名空间就应用到整个项目，不用重复using。
2. 通常创建一个专门编写全局using代码的C#文件。
3. 如果csproj中启用了`<ImplicitUsing>enable</ImplicitUsing>`，编译器会自动隐士增加对于`System`、`System.Linq`等常用命名空间的银日，不同各类型项目引入的命名空间也不一样。