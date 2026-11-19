using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class TitleCharacterDto
    {
        [Required]
        public string NConst { get; set; }

        [Required]
        public string TConst { get; set; } 

        public string Character { get; set; } 

        public int Ordering { get; set; } 
    }
}
