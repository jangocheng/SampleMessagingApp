using System;
using System.Runtime.Serialization;

namespace SampleMessagingApp.Messaging.Exceptions
{
    [Serializable]
    public class UserTopicNotRegisteredException : Exception
    {
        public UserTopicNotRegisteredException()
        {
        }

        public UserTopicNotRegisteredException(string message) : base(message)
        {
        }

        public UserTopicNotRegisteredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserTopicNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
