using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class NameBasicDetailDto
    {
        [Required]
        public string NConst { get; set; }

        public string PrimaryName { get; set; } 

        public string? BirthYear { get; set; }

        public string? DeathYear { get; set; } 

        public string? PrimaryProfession { get; set; }

        public IList<string>? KnownForTitles { get; set; }
    }
}
