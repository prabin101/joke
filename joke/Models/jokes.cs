using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace joke.Models
{
    public class jokes
    {
        public int ID { get; set; }
        public string jokequestion { get; set; }
        public string jokeanswer { get; set; }

        public jokes()
        {
             
        }
    }
}
