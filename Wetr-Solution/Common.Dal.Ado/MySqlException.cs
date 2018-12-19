using System;

namespace Common.Dal.Ado
{
    /// <summary>
    /// Custom MySqlException
    /// </summary>
    public class MySqlException : Exception
    {
        public MySqlException()
        {

        }

        public MySqlException(string message) : base(message)
        {

        }

        public MySqlException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
