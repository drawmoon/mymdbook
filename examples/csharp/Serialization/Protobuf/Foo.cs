using ProtoBuf;

namespace Serialization
{
    [ProtoContract]
    [ProtoInclude(10000, typeof(Bar))]
    public class Foo
    {
        [ProtoMember(10)]
        public string Id { get; set; }

        [ProtoMember(20)]
        public string Name { get; set; }
    }
}
