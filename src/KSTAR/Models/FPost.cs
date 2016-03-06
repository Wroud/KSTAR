using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.Models
{
    public class FPost
    {
        public int ID { get; set; }
        public int TopicID { get; set; }
        public string UserID { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual FTopic Topic { get; set; }
    }
}
