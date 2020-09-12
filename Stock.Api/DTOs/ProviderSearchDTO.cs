using System.ComponentModel.DataAnnotations;
namespace Stock.Api.DTOs
{
    public class ProviderSearchDTO
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public ActionDto Condition { get; set; }
    }
}