using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.Models
{
    public class FGroup
    {
        public int ID { get; set; }
        [Required]
        [StringLength(24, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Title { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public virtual List<FSubject> Subjects { get; set; }
    }
}
