using System;

namespace Stock.Repository.LiteDb.Exceptions
{
    /// <summary>
    /// Repository exception.
    /// </summary>
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        public RepositoryException(string message) : base(message)
        {
        }
    }
}