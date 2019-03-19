using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.ViewModels.Profile
{
    public class EditViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string confirmPassword { get; set; }
        public string ProfilePic { get; set; }
        public IEnumerable<HttpPostedFileBase> file { get; set; }
    }
}