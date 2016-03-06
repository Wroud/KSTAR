using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.Models
{
    public class FSubject
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual FGroup Group { get; set; }
        public virtual List<FTopic> Topics { get; set; }
}
}
