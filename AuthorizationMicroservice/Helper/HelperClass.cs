using AuthorizationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Helper
{
    public class HelperClass
    {
        public static List<UserModel> userlist = new List<UserModel>()
        {
            new UserModel{ userid=1,password="123562",username="Abhishek"},
            new UserModel{ userid=2,password="1235",username="Shubham"},
            new UserModel{ userid=3,password="1235987",username="Shailesh"},
            new UserModel{ userid=4,password="123562321",username="Vipin"},
            new UserModel{ userid=5,password="12356232",username="Mohit"}
        };
    }
}
