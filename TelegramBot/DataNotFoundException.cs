using System;

namespace TelegramBot
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException() : base() { }

        public DataNotFoundException(string str) : base(str) { }
        
        public DataNotFoundException(string str, Exception inner) : base(str, inner) { }
        
        protected DataNotFoundException(
            System.Runtime.Serialization.SerializationInfo si,
            System.Runtime.Serialization.StreamingContext sc) : base(si, sc) { }

        public override string ToString()
        {
            return Message;
        }
    }
}