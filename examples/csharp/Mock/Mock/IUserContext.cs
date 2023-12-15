using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock
{
    public interface IUserContext
    {
        public string Id { get; }

        public string Name { get; }
    }
}
