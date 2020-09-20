namespace Stock.Api.DTOs
{
    public class ProductSearchDTO
    { 
        public string Name { get; set; }
        public string Brand { get; set; }
        public ActionDto Condition { get; set; } = ActionDto.AND;
    }
}