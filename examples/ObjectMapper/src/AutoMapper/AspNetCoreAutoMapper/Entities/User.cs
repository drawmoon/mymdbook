using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAutoMapper.Entities
{
    public class User
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public UserSex Sex { get; set; }
    }
}
