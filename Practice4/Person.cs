using System;

namespace Practice02
{
    public class Person
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;
        private readonly string[] _westernSigns = { "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Saggitarius", "Capricorn" };
        private readonly string[] _chineseSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

        public bool IsAdult
        {
            get
            {
                var today = DateTime.Today;
                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                var b = (_dateOfBirth.Year * 100 + _dateOfBirth.Month) * 100 + _dateOfBirth.Day;
                return (a - b) / 10000 >= 18;
            }
        }

        public string SunSign
        {
            get
            {
                int day = _dateOfBirth.Day;
                int month = _dateOfBirth.Month;
                //Not used variable

                if (month == 1 || month == 4)
                    return day >= 20 ? _westernSigns[month - 1] : (month == 1 ? _westernSigns[_westernSigns.Length - 1] : _westernSigns[month - 2]);
                if (month == 2)
                    return day >= 19 ? _westernSigns[month - 1] : _westernSigns[month - 2];

                if (month == 3 || month == 5 || month == 6)
                    return day >= 21 ? _westernSigns[month - 1] : _westernSigns[month - 2];

                if (month == 7 || month == 8 || month == 9 || month == 10)
                    return day >= 23 ? _westernSigns[month - 1] : _westernSigns[month - 2];

                return day >= 22 ? _westernSigns[month - 1] : _westernSigns[month - 2];
            }
        }

        public string ChineseSign
        {
            get
            {
                var date = _dateOfBirth.Year;
                return _chineseSigns[(date - 5) % 12];
            }
        }

        public bool IsBirthday
        {
            get
            {
                return _dateOfBirth.DayOfYear == DateTime.Today.DayOfYear;
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                SignInValidator.ValidateEmail(this);
                _email = value;
            }
        }
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                SignInValidator.ValidateBirthday(this);
                _dateOfBirth = value;
            }
        }

        public Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dateOfBirth = dateOfBirth;
            new SignInValidator(this);
        }

        public Person(string firstName, string lastName, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dateOfBirth = DateTime.MinValue;
            new SignInValidator(this);
        }

        public Person(string firstName, string lastName, DateTime dateOfBirth)
        {
            _firstName = firstName;
            _lastName = lastName;
            _dateOfBirth = dateOfBirth;
            _email = null;
            new SignInValidator(this);
        }

    }
}
