using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.Models
{
    public enum BaseRoles
    {
        Administrator, User
    }
    public class ApplicationRole : IdentityRole
    {
        public int Priority { get; set; }
    }
}
