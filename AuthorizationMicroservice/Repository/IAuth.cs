using AuthorizationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Repository
{
    public interface IAuth
    {
        public UserModel AuthenticateUser(int userid, string password);
        public string GenerateJsonWebToken(UserModel user);
        public UserModel UserDetails(int userid);

    }
}
