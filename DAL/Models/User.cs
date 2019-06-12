using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }
    }
}
