using System;
using System.Collections.Generic;
using System.Text;

namespace AirTrack.Model.Account.User
{
    public class UserListModel
    {
        
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Homebase { get; set; }
        public string Phone { get; set; }
        public string PhonePrivate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public int[] SelectedRoleIds { get; set; }
    }
}
