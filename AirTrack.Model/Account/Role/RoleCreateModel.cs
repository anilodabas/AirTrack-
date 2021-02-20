using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace AirTrack.Model.Account.Role
{
    public class RoleCreateModel
    {
        [Required(ErrorMessage = "Role name must be declare")]
        [MaxLength(50,ErrorMessage ="Role name length cannot be grearter than 50")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
