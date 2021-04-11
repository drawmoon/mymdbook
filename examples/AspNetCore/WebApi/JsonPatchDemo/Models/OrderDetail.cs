using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreJsonPatch.Models
{
    [DataContract]
    public class OrderDetail
    {
        [DataMember(Name = nameof(Name))]
        public string Name { get; set; }
    }
}
