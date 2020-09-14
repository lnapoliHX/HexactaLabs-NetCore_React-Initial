using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs.Provider
{
    public class ProviderQueryDto
    {
        [RegularExpression("^[a-zA-Z]+$")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public ActionDto Condition { get; set; }
    }
}