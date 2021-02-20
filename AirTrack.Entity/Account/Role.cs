using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AirTrack.Entity.Base;

namespace AirTrack.Entity.Account
{
    public class Role : AuiditEntity
    {


        [MaxLength(60)]
        public string Name { get; set; }

        public  ICollection<User> Users { get; set; } = new HashSet<User>();

    }
}
