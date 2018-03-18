using System;

namespace Practice02
{
    class DistantPastBirthdayException : Exception
    {
        private string _message;
        private DateTime? _receivedDate;

        public DistantPastBirthdayException(string message)
        {
            _message = message;
        }

        public DistantPastBirthdayException(DateTime badDate)
        {
            _receivedDate = badDate;
            _message = $"A date from future was passed: {_receivedDate.ToString()}";
        }

        public DistantPastBirthdayException(DateTime badDate, string message)
        {
            _receivedDate = badDate;
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }
    }
}