using System;

namespace Practice02
{
    class InvalidEmailException : Exception
    {
        private string _message;

        public override string Message
        {
            get => _message;
        }

        public InvalidEmailException(string message)
        {
            _message = message;
        }
    }
}
