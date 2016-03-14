using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Dnx.TestHost;
using System.ComponentModel.DataAnnotations;

namespace KSTAR.Models
{
    public class FSubject
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0)]
        [Display(Name = "Icon")]
        public string Icon { get; set; }
        [Required]
        [StringLength(24, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public int TopicCount { get; set; }
        public int ViewsCount { get; set; }
        public int PostCount { get; set; }

        public virtual FGroup Group { get; set; }
        public virtual List<FTopic> Topics { get; set; }
}
}
