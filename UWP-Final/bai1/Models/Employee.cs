using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace bai1.Models
{
    class Employee
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string role { get; set; }
        [DataMember]
        public int birthday { get; set; }
    }
}
