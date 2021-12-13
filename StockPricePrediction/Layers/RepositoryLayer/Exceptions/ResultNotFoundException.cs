using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace RepositoryLayer.Exceptions
{
    [Serializable]
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

        protected ResultNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}