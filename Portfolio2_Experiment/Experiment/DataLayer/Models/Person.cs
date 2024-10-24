using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string ActualName { get; set; }
        public string NConst { get; set; }
        public string BirthYear { get; set; }
        public string DeathYear { get; set; }
        public string PrimaryProfession { get; set; }

      
        public string KnownForTitles { get; set; } 
        public List<KnownForTitle> KnownForTitlesList { get; set; }
    }
}
