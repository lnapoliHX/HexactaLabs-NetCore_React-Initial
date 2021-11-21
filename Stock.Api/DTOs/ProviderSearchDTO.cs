namespace Stock.Api.DTOs
{
    public class ProviderSearchDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ActionDto Action { get; set; }
    }
}
