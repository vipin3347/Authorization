using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Models
{
    public class UserModel
    {

        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
