using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AirTrack.Model.Account.User
{
   
    public class UserCreateModel:BaseUserModel
    {
        
        public string Code { get; set; }
       
    }
}
