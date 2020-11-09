using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthController : ControllerBase
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuthController));

        IAuth iauth;
        public AuthController(IAuth _iauth)
        {
            iauth = _iauth;

        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            log.Info("AuthController - Login - Authentication Initiated for User Id: " + user.userid);

            try
            {
                IActionResult response = Unauthorized();
                var uservalue = iauth.AuthenticateUser(user.userid, user.password);
                if (uservalue != null)
                {
                    var tokenString = iauth.GenerateJsonWebToken(user);
                    log.Info("AuthController - Login - Token Generated for UserId: " + user.userid);
                    response = Ok(tokenString);
                    log.Info("AuthController - Login - Authentication Successful for User Id: " + user.userid);
                }
                else
                {
                    log.Info("AuthController - Login - Authentication Unsuccessful for User Id: " + user.userid);
                }
                return response;
            }

            catch (Exception)
            {
                log.Info("AuthController - Login - BadRequest");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserDetails")]
        public IActionResult GetUserDetails(int id)
        {
            try
            {
                var result = iauth.UserDetails(id);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }

            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}