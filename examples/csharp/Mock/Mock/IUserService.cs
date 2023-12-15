using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock
{
    public interface IUserService
    {
        public IUserContext Current { get; }

        public List<IUserContext> GetOnlineUser();

        public IUserContext Switch(string id);

        public IUserContext FindUser(string name, string? tag = null);
    }
}
