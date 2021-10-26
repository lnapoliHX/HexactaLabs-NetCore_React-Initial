using System;

namespace Stock.Model.Exceptions
{
    /// <summary>
    /// Defines the model exception.
    /// </summary>
    public class ModelException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException"/> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        public ModelException(string message) : base(message)
        {
        }
    }
}
