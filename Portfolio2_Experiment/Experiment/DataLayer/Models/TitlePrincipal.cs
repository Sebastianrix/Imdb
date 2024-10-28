using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class TitlePrincipal
    {
        public string NConst { get; set; }
        public string TConst { get; set; }
        public int Ordering { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }

       
        public Person Person { get; set; }
        public TitleBasic TitleBasic { get; set; }
    }

}