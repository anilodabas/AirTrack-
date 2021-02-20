using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AirTrack.Model.Account.User
{
    public class BaseUserModel
    {
        [Required(ErrorMessage = "Name field is required")]
        [MaxLength(50, ErrorMessage = "Name length cannot be grearter than 50")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname field is required")]
        [MaxLength(50, ErrorMessage = "Surname length cannot be grearter than 50")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "BirtDate field is required")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Occupation field is required")]
        [MaxLength(50, ErrorMessage = "Occupation name length cannot be grearter than 50")]
        public string Occupation { get; set; }
        [Required(ErrorMessage = "Email field is required")]
        [MaxLength(50, ErrorMessage = "Email length cannot be grearter than 50")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        [MaxLength(50, ErrorMessage = "Password character length cannot be grearter than 50")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Homebase field is required")]
        [MaxLength(50, ErrorMessage = "Homebase character length cannot be grearter than 50")]
        public string HomeBase { get; set; }
        [MaxLength(50, ErrorMessage = "Phone number length cannot be grearter than 50")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Phoneprivate field is required")]
        [MaxLength(50, ErrorMessage = "Phoneprivate number length cannot be grearter than 50")]
        public string PhonePrivate { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Role section is mandatory!")]
        public int[] SelectedRoleId { get; set; }
    }
}
