using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace School_Management_System_Application.Areas.Identity.Data;

// Add profile data for application users by adding properties to the School_Management_System_ApplicationUser class
public class User : IdentityUser
{
    public static implicit operator User(List<User> v)
    {
        throw new NotImplementedException();
    }
}

