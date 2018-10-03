using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjectManagement.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string UserName { get; set; }

        public string Email {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public bool BrowserRemembered { get; set; }
    }
}
