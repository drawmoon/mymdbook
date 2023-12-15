using ProtoBuf;
using ProtoBuf.Meta;
using Serialization;
using System;
using System.Diagnostics;
using System.IO;


RuntimeTypeModel.Default.AllowParseableTypes = true;


var foo = new Foo
{
    Id = Guid.NewGuid().ToString(),
    Name = "foo"
};

using (var ms = new MemoryStream())
{
    Serializer.Serialize(ms, foo);
    ms.Position = 0;
    ms.Flush();

    var deserialized = Serializer.Deserialize<Foo>(ms);
    Debug.Assert(deserialized.Id == foo.Id);
}


var bar = new Bar
{
    Id = Guid.NewGuid().ToString(),
    Name = "bar",
    //BarArray = new[]
    //{
    //    new Bar
    //    {
    //        Id = Guid.NewGuid().ToString(),
    //        Name = "bar",
    //    }
    //}
};

using (var ms = new MemoryStream())
{
    Serializer.Serialize(ms, bar);
    ms.Position = 0;
    ms.Flush();

    var deserialized = Serializer.Deserialize<Bar>(ms);
    Debug.Assert(deserialized.Id == bar.Id);
}
