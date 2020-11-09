using AuthorizationMicroservice.Controllers;
using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationTesting
{
    public class Tests
    {
        List<UserModel> user;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsTokenNotNull_When_UserCredentialsAreValid()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();

            var obj = new Auth(config.Object);

            var res = obj.AuthenticateUser(1, "123562");

            Assert.IsNotNull(res);
        }

        [Test]
        public void IsTokenNull_When_UserCredentialsAreInvalid()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();

            var obj = new Auth(config.Object);

            var res = obj.AuthenticateUser(1, "123456");

            Assert.IsNull(res);
        }

        [SetUp]
        public void Setup1()
        {
            user = new List<UserModel>()
            {
                new UserModel{ userid=1,password="123562",username="Abhishek"},
                new UserModel{ userid=2,password="1235",username="Shubham"},
                new UserModel{ userid=3,password="1235987",username="Shailesh"}
            };
        }

        [Test]
        public void GetUserDetails_ValidInput_ReturnsOkRequest()
        {
            int id = 1;

            var mock = new Mock<IAuth>();

            mock.Setup(x => x.UserDetails(id)).Returns((user.Where(x => x.userid == id)).FirstOrDefault());

            AuthController obj = new AuthController(mock.Object);

            var data = obj.GetUserDetails(id);

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }
      
        [Test]
        public void GetUserDetails_InvalidInput_ReturnsNotFoundResult()
        {
            int id = 6;

            var mock = new Mock<IAuth>();

            mock.Setup(x => x.UserDetails(id)).Returns((user.Where(x => x.userid == id)).FirstOrDefault());

            AuthController obj = new AuthController(mock.Object);

            var data = obj.GetUserDetails(id);

            var res = data as NotFoundResult;
            
            Assert.AreEqual(404, res.StatusCode);        
        }
    }
}