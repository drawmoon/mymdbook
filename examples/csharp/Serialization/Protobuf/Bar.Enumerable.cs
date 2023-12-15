using System;
using System.Collections;
using System.Collections.Generic;

namespace Serialization
{
    public partial class Bar : IEnumerable<Baz>
    {
        public IEnumerator<Baz> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
