using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice02
{
    internal class SignInValidator
    {
        private Person _person;

        public SignInValidator(Person person)
        {
            _person = person;
            Validate();
        }

        public static void ValidateBirthday(Person person)
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

        public static void ValidateEmail(Person person)
        {
            if (!CorrectEmail(person))
            {
                throw new InvalidEmailException("An email you are trying to pass is not supported!");
            }
        }

        public static bool CorrectEmail(Person person)
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
