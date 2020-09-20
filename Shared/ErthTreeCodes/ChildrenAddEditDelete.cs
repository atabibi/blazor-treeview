using System;
using System.Linq;

namespace T00.Shared.ErthTreeCodes
{
    public static class ChildrenAddEditDelete
    {


        public static void DeleteAChild(Person movareth, Person person)
        {
            var personParentFound = FindAChildPerson(movareth, person);

            if (personParentFound == null)
            {
                Console.WriteLine("گره والد یافت  نشده");
                return;
            }

            if (personParentFound.Children.Remove(person))
            {
                Console.WriteLine($"{person.FullName} حذف شد");
            }
            else
            {
                Console.WriteLine($"{person.FullName} حذف نشد");
            }
        }
        static Person FindAChildPerson(Person parent, Person child)
        {
            var found = parent.Children?.SingleOrDefault(p => p == child);
            if (found != null)
            {
                return parent;
            }

            if (parent.Children != null)
            {
                Person parentFound = null;
                foreach (var p in parent.Children)
                {
                    parentFound = FindAChildPerson(p, child);
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