using ProtoBuf;

namespace Serialization
{
    [ProtoContract(IgnoreListHandling = true)] // 将此类型不视为列表，因为该类在分部类中继承了 IEnumerable<T>
    public partial class Bar : Foo
    //, IBarEnumerable // 当作为子类并标记了 ProtoInclude，如果需要实现 IEnumerable<T> 等，会抛出 ProtoBuf 的异常”列表、集合等具有内在行为，无法作为子类“，使用此方案替代
    {
        //[ProtoMember(10)]
        //public Bar[] BarArray { get; set; } // 不支持嵌套列表，如果需要该方案，修改 IEnumerable<T> 的继承为 IBarEnumerable 的实现方案，https://github.com/protobuf-net/protobuf-net/issues/706

        [ProtoMember(20)]
        public Baz[] BazArray { get; set; }

        //IEnumerator<Bar> IBarEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }

    //public interface IBarEnumerable
    //{
    //    IEnumerator<Bar> GetEnumerator();

    //    IEnumerable<Bar> GetEnumerable()
    //    {
    //        return new BarEnumerable(this);
    //    }
    //}

    //class BarEnumerable : IEnumerable<Bar>
    //{
    //    private readonly IBarEnumerable enumerable;

    //    public BarEnumerable(IBarEnumerable enumerable)
    //    {
    //        this.enumerable = enumerable;
    //    }

    //    public IEnumerator<Bar> GetEnumerator()
    //    {
    //        return enumerable.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
}
