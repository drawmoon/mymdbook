using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDemo
{
    public interface IUserContext
    {
        public string Id { get; }

        public string Name { get; }
    }
}
