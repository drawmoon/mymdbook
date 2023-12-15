using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;


// 响应式编程，观察者模式

var su = new Subject<string>();
su.Subscribe(x => Console.WriteLine(x)); // 订阅

for (int i = 0; i < 4; i++)
{
    su.OnNext(i.ToString()); // 通知观察员
}

su.Dispose();
