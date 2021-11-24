using System;
using Stock.Model.Base;

namespace Stock.Model.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is an attempt to save an instance of a class that implements
    /// the inteface <see cref="IUniquelyNamedEntity"/> and the instance's Name is not unique.
    /// </summary>
    public class NotUniqueNameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotUniqueNameException"/> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        public NotUniqueNameException(string message) : base(message)
        {

        }
    }
}
