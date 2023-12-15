using ProtoBuf;

namespace Serialization
{
    [ProtoContract]
    public class Baz
    {
        [ProtoMember(10)]
        public string Id { get; set; }

        [ProtoMember(20)]
        public string Name { get; set; }
    }
}
