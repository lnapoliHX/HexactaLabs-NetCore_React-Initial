namespace Stock.Api.DTOs
{
    public class GenericResultDTO<T>
    {
        public GenericResultDTO(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
    }
}
