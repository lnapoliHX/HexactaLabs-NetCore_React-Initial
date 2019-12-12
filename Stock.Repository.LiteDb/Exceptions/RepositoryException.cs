using System;

namespace Stock.Repository.LiteDb.Exceptions
{
    public class RepositoryException: Exception
    {
        public RepositoryException(string message): base(message)
        {
        }
    }
}