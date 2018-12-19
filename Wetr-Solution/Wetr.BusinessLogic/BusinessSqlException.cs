using System;

namespace Wetr.BusinessLogic
{
    /// <summary>
    /// Custom MySqlException
    /// </summary>
    public class BusinessSqlException : Exception
    {
        public BusinessSqlException()
        {

        }

        public BusinessSqlException(string message) : base(message)
        {

        }

        public BusinessSqlException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
