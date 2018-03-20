using System.Linq;

namespace Practice4
{
    internal static class FilterHelper
    {
        public static User[] FilterByChineeseSign(User[] people, string signName)
        {
            return (from p in people where p.ChineseSign.ToLower().Equals(signName.ToLower()) select p).ToArray();
        }

        public static User[] FilterBySunSign(User[] people, string signName)
        {
            return (from p in people where p.SunSign.ToLower().Equals(signName.ToLower()) select p).ToArray();
        }

        public static User[] FilterByAge(User[] people, int age, bool moreThan=true)
        {
            return (from p in people where (moreThan ? p.Age > age : p.Age < age) select p).ToArray();
        }
    }
}
