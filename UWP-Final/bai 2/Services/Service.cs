using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dm1.Services
{
    class Service
    {
        private IUserService _tUserService;

        public IUserService UserService()
        {
            if (this._tUserService == null)
            {
                this._tUserService = new UserService();
            }
            return this._tUserService;
        }
    }
}
