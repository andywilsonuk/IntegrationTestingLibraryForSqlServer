using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ValidationException : Exception
    {
        public ValidationException()
            :base()
        {
        }
       
        public ValidationException(string message)
            : base(message)
        {

        }
       
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public ValidationException(string message, Exception innerException)
            : base (message, innerException)
        {
        }
    }
}
