using System;

namespace Stock.Model.Exceptions
{
    public class ModelException : Exception
    {
        public ModelException(string message) : base(message)
        {
        }
    }
}
