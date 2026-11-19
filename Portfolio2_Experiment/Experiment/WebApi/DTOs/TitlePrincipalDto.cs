using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class TitlePrincipalDto
    {
        [Required]
        public string TConst { get; set; } 

        [Required]
        public string NConst { get; set; } 

        public int Ordering { get; set; } 

        public string Category { get; set; } 

        public string Job { get; set; } 
    }
}
