using System.Linq;

namespace Practice4
{
    internal static class SortHelper
    {
        public static User[] Sorted(User[] people, bool ascending, string property)
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

        private static User[] SortedByFirstName(User[] people, bool ascending)
        {
            if (ascending) {
                return (from p in people orderby p.FirstName ascending select p).ToArray();
            }
            return (from p in people orderby p.FirstName descending select p).ToArray();
        }

        private static User[] SortedByLastName(User[] people, bool ascending)
        {
            if (ascending)
            {
                return (from p in people orderby p.LastName ascending select p).ToArray();
            }
            return (from p in people orderby p.LastName descending select p).ToArray();
        }

        private static User[] SortedByDateOfBirth(User[] people, bool ascending)
        {
            if (ascending)
            {
                return (from p in people orderby p.DateOfBirth ascending select p).ToArray();
            }
            return (from p in people orderby p.DateOfBirth descending select p).ToArray();
        }
    }
}
