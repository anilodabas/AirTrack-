using System;
using System.Collections.Generic;
using System.Text;
using AirTrack.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace AirTrack.Entity.Account
{
    public class User : AuiditEntity
    {virtual  
        
         public string Code { get; set; }
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname field is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "BirthDate field is required")]
        public DateTime BirthDate { get; set; }

        public string Occupation { get; set; }
        [Required(ErrorMessage = "Email field is required")]
        public string Email  {get;set;}
        [Required(ErrorMessage ="Password field is required")]
        public string Password { get; set; }
   
        public string Homebase  { get;set;}
        [Required(ErrorMessage = "Phone field is required")]
        public string Phone { get;set;}
        public string PhonePrivate { get;set;}

        public  ICollection<Role> Roles { get; set; } = new HashSet<Role>();

    }
}
