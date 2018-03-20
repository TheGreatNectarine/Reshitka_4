using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice4
{
    internal class SignInValidator
    {
        private User _person;

        public SignInValidator(User person)
        {
            _person = person;
            Validate();
        }

        public static void ValidateBirthday(User person)
        {
            if (person.DateOfBirth >= DateTime.Today)
            {
                throw new FutureBirthdayException("You cannot register unborn people!");
            }
            if (DateTime.Today.Year - person.DateOfBirth.Year > 135)
            {
                throw new DistantPastBirthdayException("The user is dead. Do not register dead men.");
            }
        }

        public static void ValidateEmail(User person)
        {
            if (!CorrectEmail(person))
            {
                throw new InvalidEmailException("An email you are trying to pass is not supported!");
            }
        }

        public static bool CorrectEmail(User person)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(person.Email);
                return addr.Address == person.Email;
            }
            catch
            {
                return false;
            }
        }

        private void Validate()
        {
            CheckBDay();
            CheckEmail();
        }

        private void CheckBDay()
        {
            if (_person.DateOfBirth >= DateTime.Today)
            {
                throw new FutureBirthdayException("You cannot register unborn people!");
            }
            if(DateTime.Today.Year - _person.DateOfBirth.Year > 135)
            {
                throw new DistantPastBirthdayException("The user is dead. Do not register dead men.");
            }
        }

        private void CheckEmail()
        {
            if (!CorrectEmail(_person))
            {
                throw new InvalidEmailException("An email you are trying to pass is not supported!");
            }
        }
    }
}
