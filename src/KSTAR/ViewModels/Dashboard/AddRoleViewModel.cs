using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.ViewModels.Account
{
    public class AddRoleViewModel
    {
        [Required]
        [StringLength(24, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
