using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GUI_Geruest
{
    class eventLogListe
    {
        public long id { get; set; }
        public string catagory { get; set; }
        public string source { get; set; }
        public DateTime timeWritte { get; set; }
        public  string type { get; set; }
        public int index { get; set; }
        public string message { get; set; }
        public string user { get; set; }
        public string pc { get; set; }
        public DateTime timeGen { get; set; }
    }


}