using System;
using System.Linq;

namespace T00.Shared.ErthTreeCodes
{
    public static class AmamAkhvalAddEditDelete
    {
        public static void DeleteAnAmmOrKhall(Person movareth, Person person)
        {
            var personParentFound = FindAnAmmOrKhallPerson(movareth, person);

            if (personParentFound == null)
            {
                Console.WriteLine("گره والد یافت  نشده");
                return;
            }

            bool success;
            if (personParentFound.Tabagheh == TabaghehType.Tabagheh3) // parent is not movareth
            {
                success = personParentFound.Children.Remove(person);
            }
            else    // parent is movareth
            {
                success = personParentFound.Amam.Remove(person);
                if (success == false)
                {
                    success = personParentFound.Akhval.Remove(person);
                }
            }

            if (success)
            {
                Console.WriteLine($"{person.FullName} حذف شد");
            }
            else
            {
                Console.WriteLine($"{person.FullName} حذف نشد");
            }
        }

        static Person FindAnAmmOrKhallPerson(Person parent, Person amAkhOrHisChild)
        {
            Person found = null;

            if (parent.Tabagheh == TabaghehType.Tabagheh3) // parent is Not Movareth
            {
                found = parent.Children?.SingleOrDefault(p => p == amAkhOrHisChild);
            }
            else // parent is Movareth
            {
                found = parent.Amam?.SingleOrDefault(p => p == amAkhOrHisChild);
                if (found == null)
                {
                    found = parent.Akhval?.SingleOrDefault(p => p == amAkhOrHisChild);
                }
            }

            if (found != null)
            {
                return parent;
            }

            if (parent.Amam != null)
            {
                Person parentFound = null;
                foreach (var p in parent.Amam)
                {
                    parentFound = FindAnAmmOrKhallPerson(p, amAkhOrHisChild);
                    if (parentFound != null)
                    {
                        return parentFound;
                    }
                }
            }

            if (parent.Akhval != null)
            {
                Person parentFound = null;
                foreach (var p in parent.Akhval)
                {
                    parentFound = FindAnAmmOrKhallPerson(p, amAkhOrHisChild);
                    if (parentFound != null)
                    {
                        return parentFound;
                    }
                }
            }

            return null;
        }
    }
}