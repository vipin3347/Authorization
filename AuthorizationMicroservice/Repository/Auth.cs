using AuthorizationMicroservice.Helper;
using AuthorizationMicroservice.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Repository
{
    public class Auth : IAuth
    {
        private IConfiguration iconfig;
        public Auth(IConfiguration _config)
        {
            iconfig = _config;
        }

        public UserModel AuthenticateUser(int userid, string password)
        {
            foreach (var user in HelperClass.userlist)
            {
                if (user.userid == userid && user.password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public string GenerateJsonWebToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfig["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(iconfig["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public UserModel UserDetails(int userid)
        {
            foreach (var user in HelperClass.userlist)
            {
                if (user.userid == userid)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
