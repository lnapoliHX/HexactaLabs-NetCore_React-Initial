namespace Stock.Api.DTOs
{
    public class ProviderSearchDTO
    {
        public string id { get; set; }

        public string Name { get; set; }

        public ActionDto Condition { get; set; } 
    }
}