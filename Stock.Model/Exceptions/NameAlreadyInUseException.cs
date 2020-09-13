namespace Stock.Model.Exceptions
{
    public class NameAlreadyInUseException : ModelException
    {
        public NameAlreadyInUseException(string name) : base($"The name {name} is already in use")
        {
        }
    }
}