using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dm1.Models
{
    class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Dictionary<string, string> Validate()
        {
            var errs = new Dictionary<string, string>();

            if (Id.Equals(0))
            {
                errs.Add("Error", "Id empty");
            } 
            else if (string.IsNullOrEmpty(UserName))
            {
                errs.Add("Error", "UserName empty");
            }
            else if (string.IsNullOrEmpty(Password))
            {
                errs.Add("Error", "PasswordName empty");
            }

            return errs;
        }
    }
}
