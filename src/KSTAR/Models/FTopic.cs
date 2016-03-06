using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.Models
{
    public class FTopic
    {
        public int ID { get; set; }
        public int SubjectID { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public virtual FSubject Subject { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<FPost> Posts { get; set; }
    }
}
