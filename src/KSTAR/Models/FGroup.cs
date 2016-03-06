using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.Models
{
    public class FGroup
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual List<FSubject> Subjects { get; set; }

    }
}
