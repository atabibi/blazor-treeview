using System;

namespace T00.Shared.ErthTreeCodes
{
    public static class ParentsAddEditDelete
    {
        public static void DeleteAParent(Person movareth, Person person)
        {
            if (movareth.Parents.Remove(person))
            {
                Console.WriteLine($"{person.FullName} حذف شد");
            }
            else
            {
                Console.WriteLine($"{person.FullName} حذف نشد");
            }
        }
    }
}