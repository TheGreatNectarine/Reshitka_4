using System.Collections.Generic;
using System.Linq;

namespace Practice4
{
    internal static class FilterHelper
    {
        public static List<User> FilterByChineeseSign(List<User> people, string signName)
        {
            return (from p in people where p.ChineseSign.ToLower().Equals(signName.ToLower()) select p).ToList();
        }

        public static List<User> FilterBySunSign(List<User> people, string signName)
        {
            return (from p in people where p.SunSign.ToLower().Equals(signName.ToLower()) select p).ToList();
        }

        public static List<User> FilterByAge(List<User> people, int age, bool moreThan=true)
        {
            return (from p in people where (moreThan ? p.Age > age : p.Age < age) select p).ToList();
        }
    }
}
