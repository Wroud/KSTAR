using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.Models
{
    public class FTopic
    {
        public int ID { get; set; }
        public int SubjectID { get; set; }
        public string UserID { get; set; }
        public int ViewCount { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0)]
        [Display(Name = "Desctription")]
        public string Description { get; set; }
        [Required]
        [StringLength(32000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 50)]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public DateTime Update { get; set; }
        public DateTime Active { get; set; }

        public virtual FSubject Subject { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<FPost> Post { get; set; }
    }
}
