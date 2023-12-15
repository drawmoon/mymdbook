using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectMapper.Entities;

namespace ObjectMapper.Dto
{
    public class UserDto
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public UserSex Sex { get; set; }
    }
}
