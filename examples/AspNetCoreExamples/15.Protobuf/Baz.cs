using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProtobufDemo
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
