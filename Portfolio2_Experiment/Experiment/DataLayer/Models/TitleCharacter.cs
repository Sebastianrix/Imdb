﻿using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class TitleCharacter
    {

        [Key]
        public string NConst { get; set; }
        public string? TConst { get; set; }
        public string? Character { get; set; }
       
        public int Ordering { get; set; }

    
        public Person Person { get; set; }
        public TitleBasic? TitleBasic  { get; set; }


    }
}
