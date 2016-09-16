using System;
using System.Runtime.Serialization;

namespace Bridge_App
{
    [Serializable]
    internal class InvalidPeopleValueException : Exception
    {
        public InvalidPeopleValueException()
        {
        }

        public InvalidPeopleValueException(string message) : base(message)
        {
        }

        public InvalidPeopleValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPeopleValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}