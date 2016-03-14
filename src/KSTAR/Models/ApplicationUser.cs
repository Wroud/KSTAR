using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KSTAR.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Avatar { get; set; }
        public DateTime RegisterDate { get; set; }

        public virtual ForumUser ForumUser { get; set; }
        public virtual List<FTopic> ForumTopic { get; set; }
        public virtual List<FPost> ForumPost { get; set; }
    }
}
