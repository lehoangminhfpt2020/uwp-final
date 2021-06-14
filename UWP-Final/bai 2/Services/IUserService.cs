using Dm1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dm1.Services
{
    interface IUserService
    {
        bool  Create(User user);
        bool CheckLogin(User user);
    }
}
