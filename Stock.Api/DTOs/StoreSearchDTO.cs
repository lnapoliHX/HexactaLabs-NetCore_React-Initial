namespace Stock.Api.DTOs
{
    public class StoreSearchDTO
    {
        public string Address { get; set; }

        public string Name { get; set; }

        public ActionDto Condition { get; set; } 
    }
}