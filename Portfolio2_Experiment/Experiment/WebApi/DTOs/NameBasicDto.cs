using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class NameBasicDto
    {
        [Required]
        public string NConst { get; set; } 

        public string ActualName { get; set; }

        public string? BirthYear { get; set; } 

        public string? DeathYear { get; set; } 

    }
}
