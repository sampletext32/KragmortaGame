using System;

namespace MainApp
{
    public class KragException : Exception
    {
        public KragException()
        {
        }

        public KragException(string message) : base(message)
        {
        }
    }
}