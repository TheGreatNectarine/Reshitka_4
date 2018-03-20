using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;

namespace Practice4
{
    [Serializable]
    public class User
    {
        internal const string filename = "Users.dat";
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;
        private static string _DATA_FILEPATH = "users";
        private static string _PERSON_FILE_TEMPLATE = "User{0}.bin";
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

        public int Age
        {
            get
            {
                TimeSpan tsAge = DateTime.Now.Subtract(DateOfBirth);
                return new DateTime(tsAge.Ticks).Year - 1;
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

        public User(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dateOfBirth = dateOfBirth;
            new SignInValidator(this);
        }

        public User(string firstName, string lastName, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dateOfBirth = DateTime.MinValue;
            new SignInValidator(this);
        }

        public User(string firstName, string lastName, DateTime dateOfBirth)
        {
            _firstName = firstName;
            _lastName = lastName;
            _dateOfBirth = dateOfBirth;
            _email = null;
            new SignInValidator(this);
        }

        public void CopyUser(User u)
        {
            _firstName = u.FirstName;
            _lastName = u.LastName;
            _email = u.Email;
            _dateOfBirth = u.DateOfBirth;
        }

        private void Serialize([NotNull] string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Directory.CreateDirectory(Path.GetDirectoryName(filename) ?? throw new InvalidOperationException());
            Stream stream = new FileStream(path: filename,
                mode: FileMode.Create,
                access: FileAccess.Write,
                share: FileShare.None);
            formatter.Serialize(serializationStream: stream, graph: this);
            stream.Close();
        }

        private static User Deserialize(string filename)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);
                var person = formatter.Deserialize(stream) as User;
                stream.Close();
                return person;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async void Preload(List<User> users)
        {
            await Task.Run(() =>
            {
                if (!Directory.Exists(_DATA_FILEPATH))
                {
                    Directory.CreateDirectory(_DATA_FILEPATH);
                    users = UserGenerator.GenerateUsers(50);
                    SerializeAll(users);
                }
                else
                {
                    var x = Directory.EnumerateFiles(_DATA_FILEPATH).Select(Deserialize);
                    users.AddRange(x);
                }
            });
        }

        public static void SerializeAll([NotNull] List<User> users)
        {
            var i = 0;
            users.ForEach(delegate (User u)
            {
                u.Serialize(Path.Combine(_DATA_FILEPATH, string.Format(_PERSON_FILE_TEMPLATE, i++)));
            });
            string extraFile;
            while(File.Exists(extraFile = Path.Combine(_DATA_FILEPATH, string.Format(_PERSON_FILE_TEMPLATE, i++))))
            {
                File.Delete(extraFile);
            }
        }

        public delegate void SerializeAllDelegate([NotNull] List<User> users);

    }

    internal static class UserGenerator
    {
        private static string[] names = { "Michael", "James", "John", "Robert", "David", "William", "Mary", "Christopher", "Joseph", "Richard", "Daniel", "Thomas", "Matthew", "Jennifer", "Charles", "Anthony", "Patricia", "Linda", "Mark", "Elizabeth", "Joshua", "Steven", "Andrew", "Kevin", "Brian", "Barbara", "Jessica", "Jason", "Susan", "Timothy", "Paul", "Kenneth", "Lisa", "Ryan", "Sarah", "Karen", "Jeffrey", "Donald", "Ashley", "Eric", "Jacob", "Nicholas", "Jonathan", "Ronald", "Michelle", "Kimberly", "Nancy", "Justin", "Sandra", "Amanda", "Brandon", "Stephanie", "Emily", "Melissa", "Gary", "Edward", "Stephen", "Scott", "George", "Donna", "Jose", "Rebecca", "Deborah", "Laura", "Cynthia", "Carol", "Amy", "Margaret", "Gregory", "Sharon", "Larry", "Angela", "Maria", "Alexander", "Benjamin", "Nicole", "Kathleen", "Patrick", "Samantha", "Tyler", "Samuel", "Betty", "Brenda", "Pamela", "Aaron", "Kelly", "Heather", "Rachel", "Adam", "Christine", "Zachary", "Debra", "Katherine", "Dennis", "Nathan", "Christina", "Julie", "Jordan", "Kyle", "Anna" };
        private static string[] surnames = { "SMITH", "JOHNSON", "WILLIAMS", "BROWN", "JONES", "GARCIA", "RODRIGUEZ", "MILLER", "MARTINEZ", "DAVIS", "HERNANDEZ", "LOPEZ", "GONZALEZ", "WILSON", "ANDERSON", "THOMAS", "TAYLOR", "LEE", "MOORE", "JACKSON", "PEREZ", "MARTIN", "THOMPSON", "WHITE", "SANCHEZ", "HARRIS", "RAMIREZ", "CLARK", "LEWIS", "ROBINSON", "WALKER", "YOUNG", "HALL", "ALLEN", "TORRES", "NGUYEN", "WRIGHT", "FLORES", "KING", "SCOTT", "RIVERA", "GREEN", "HILL", "ADAMS", "BAKER", "NELSON", "MITCHELL", "CAMPBELL", "GOMEZ", "CARTER", "ROBERTS", "DIAZ", "PHILLIPS", "EVANS", "TURNER", "REYES", "CRUZ", "PARKER", "EDWARDS", "COLLINS", "STEWART", "MORRIS", "MORALES", "ORTIZ", "GUTIERREZ", "MURPHY", "ROGERS", "COOK", "KIM", "MORGAN", "COOPER", "RAMOS", "PETERSON", "GONZALES", "BELL", "REED", "BAILEY", "CHAVEZ", "KELLY", "HOWARD", "RICHARDSON", "WARD", "COX", "RUIZ", "BROOKS", "WATSON", "WOOD", "JAMES", "MENDOZA", "GRAY", "BENNETT", "ALVAREZ", "CASTILLO", "PRICE", "HUGHES", "VASQUEZ", "SANDERS", "JIMENEZ", "LONG", "FOSTER" };
        
        public static List<User> GenerateUsers(int preferableCapacity)
        {
            var res = new List<User>();
            var gen = new Random();
            for (var i = 0; i < names.Length && i < surnames.Length && i < preferableCapacity; i++)
            {
                res.Add(new User(names[i], surnames[i], $"{names[i]}@{surnames[i]}.com", RandomDate(gen)));
            }
            return res;
        }

        private static DateTime RandomDate(Random r)
        {
            var year = r.Next(-60, -10);
            var month = r.Next(-12, 0);
            var day = r.Next(-31,0);
            return DateTime.Now.AddYears(year).AddMonths(month).AddDays(day);
        }
    }
}
