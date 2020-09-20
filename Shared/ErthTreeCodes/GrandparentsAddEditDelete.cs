using System;
using System.Linq;

namespace T00.Shared.ErthTreeCodes
{
    public static class GrandparentsAddEditDelete
    {
        public static void DeleteAGrandParent(Person movareth, Person person)
        {
            var personParentFound = FindAGrandParentPerson(movareth, person);

            if (personParentFound == null)
            {
                Console.WriteLine("گره والد یافت  نشده");
                return;
            }

            if (personParentFound.SubNodeType == SubNodeType.Grandparents)
            {
                if (personParentFound.Parents.Remove(person))
                {
                    Console.WriteLine($"{person.FullName} حذف شد");
                }
                else
                {
                    Console.WriteLine($"{person.FullName} حذف نشد");
                }
            }
            else
            {
                if (personParentFound.Ajdad.Remove(person))
                {
                    Console.WriteLine($"{person.FullName} حذف شد");
                }
                else
                {
                    Console.WriteLine($"{person.FullName} حذف نشد");
                }
            }
        }
        static Person FindAGrandParentPerson(Person person, Person parent)
        {
            Person found = null;

            if (person.SubNodeType != SubNodeType.Grandparents) //خود مورث
            {
                found = person.Ajdad?.SingleOrDefault(p => p == parent);
            }
            else // والد جد یا والد والد جد...
            {
                found = person.Parents?.SingleOrDefault(p => p == parent);
            }

            if (found != null)
            {
                return person;
            }

            if (person.SubNodeType != SubNodeType.Grandparents) //خود مورث
            {
                if (person.Ajdad != null)
                {
                    Person parentHasFound = null;
                    foreach (var p in person.Ajdad)
                    {
                        parentHasFound = FindAParentPerson(p, parent);
                        if (parentHasFound != null)
                        {
                            return parentHasFound;
                        }
                    }
                }
            }
            else // والد جد یا والد والد جد...
            {
                if (person.Parents != null)
                {
                    Person parentHasFound = null;
                    foreach (var p in person.Parents)
                    {
                        parentHasFound = FindAParentPerson(p, parent);
                        if (parentHasFound != null)
                        {
                            return parentHasFound;
                        }
                    }
                }
            }


            return null;
        }

        static Person FindAParentPerson(Person parent, Person child)
        {
            var found = parent.Parents?.SingleOrDefault(p => p == child);
            if (found != null)
            {
                return parent;
            }

            if (parent.Parents != null)
            {
                foreach (var p in parent.Parents)
                {
                    return FindAParentPerson(p, child);
                }
            }

            return null;
        }
    }
}