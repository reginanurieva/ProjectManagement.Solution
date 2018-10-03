using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models.ManageViewModels
{
    public class EditUserInfoViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName {get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName {get; set;}

        [Required]
        [Display(Name = "LastName")]
        public string LastName {get; set;}
    }
}
