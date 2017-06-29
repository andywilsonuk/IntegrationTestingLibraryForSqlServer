using System;
using System.Runtime.Serialization;

namespace IntegrationTestingLibraryForSqlServer
{
    public class EquivalenceException : Exception
    {
        public EquivalenceException()
            :base()
        {
        }
       
        public EquivalenceException(string message)
            : base(message)
        {

        }
       
        protected EquivalenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public EquivalenceException(string message, Exception innerException)
            : base (message, innerException)
        {
        }
    }
}
