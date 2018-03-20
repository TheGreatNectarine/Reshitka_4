using System.Collections.Generic;
using System.Linq;

namespace Practice4
{
    internal static class SortHelper
    {
        public static List<User> Sorted(List<User> people, bool ascending, string property)
        {
            switch (property.ToLower())
            {
                case "first name":
                    return SortedByFirstName(people, ascending);
                case "last name":
                    return SortedByLastName(people, ascending);
                case "date of birth":
                    return SortedByDateOfBirth(people, ascending);
                default: return people;
            }
        }

        private static List<User> SortedByFirstName(List<User> people, bool ascending)
        {
            if (ascending) {
                return (from p in people orderby p.FirstName ascending select p).ToList();
            }
            return (from p in people orderby p.FirstName descending select p).ToList();
        }

        private static List<User> SortedByLastName(List<User> people, bool ascending)
        {
            if (ascending)
            {
                return (from p in people orderby p.LastName ascending select p).ToList();
            }
            return (from p in people orderby p.LastName descending select p).ToList();
        }

        private static List<User> SortedByDateOfBirth(List<User> people, bool ascending)
        {
            if (ascending)
            {
                return (from p in people orderby p.DateOfBirth ascending select p).ToList();
            }
            return (from p in people orderby p.DateOfBirth descending select p).ToList();
        }
    }
}
