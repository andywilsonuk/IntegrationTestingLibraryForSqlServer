using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
