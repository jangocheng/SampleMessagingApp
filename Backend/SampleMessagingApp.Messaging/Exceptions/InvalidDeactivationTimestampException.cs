using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SampleMessagingApp.Messaging.Exceptions
{
    public class InvalidDeactivationTimestampException : Exception
    {
        public InvalidDeactivationTimestampException()
        {
        }

        public InvalidDeactivationTimestampException(string message) : base(message)
        {
        }

        public InvalidDeactivationTimestampException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDeactivationTimestampException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
