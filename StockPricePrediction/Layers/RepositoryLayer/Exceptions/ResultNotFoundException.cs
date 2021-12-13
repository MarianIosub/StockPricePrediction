using System;
using System.Globalization;

namespace RepositoryLayer.Exceptions
{
    public class ResultNotFoundException : Exception
    {
        public ResultNotFoundException()
        {
        }

        public ResultNotFoundException(string message) : base(message)
        {
        }

        public ResultNotFoundException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}