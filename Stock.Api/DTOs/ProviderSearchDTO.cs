namespace Stock.Api.DTOs
{
    public class ProviderSearchDTO
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public ActionDto Condition { get; set; }
    }
}