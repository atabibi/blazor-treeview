using System;
using System.Linq;

namespace T00.Shared.ErthTreeCodes
{
    public static class BrothersSistersAddEditDelete
    {
        public static void DeleteABrotherOrSister(Person movareth, Person person)
        {
            var personParentFound = FindABrotherOrSisterPerson(movareth, person);

            if (personParentFound == null)
            {
                Console.WriteLine("گره والد یافت  نشده");
                return;
            }

            bool success;
            if (personParentFound.Tabagheh == TabaghehType.Tabagheh2) // parent is not movareth
            {
                success = personParentFound.Children.Remove(person);
            }
            else    // parent is movareth
            {
                success = personParentFound.BrothersAndSisters.Remove(person);
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

        static Person FindABrotherOrSisterPerson(Person parent, Person bsOrHisChild)
        {
            Person found = null;

            if (parent.Tabagheh == TabaghehType.Tabagheh2) // parent is Not Movareth
            {
                found = parent.Children?.SingleOrDefault(p => p == bsOrHisChild);
                
                if (found != null)
                {
                    return parent;
                }
                
                if (parent.Children != null)
                {
                    Person parentFound = null;
                    foreach (var p in parent.Children)
                    {
                        parentFound = FindABrotherOrSisterPerson(p, bsOrHisChild);
                        if (parentFound != null)
                        {
                            return parentFound;
                        }
                    }
                }
            }
            else // parent is Movareth
            {
                found = parent.BrothersAndSisters?.SingleOrDefault(p => p == bsOrHisChild);

                if (found != null)
                {
                    return parent;
                }

                if (parent.BrothersAndSisters != null)
                {
                    Person parentFound = null;
                    foreach (var p in parent.BrothersAndSisters)
                    {
                        parentFound = FindABrotherOrSisterPerson(p, bsOrHisChild);
                        if (parentFound != null)
                        {
                            return parentFound;
                        }
                    }
                }
            }


            return null;
        }

    }
}