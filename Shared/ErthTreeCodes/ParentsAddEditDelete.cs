using System;
using System.Collections.Generic;
using System.Linq;

namespace T00.Shared.ErthTreeCodes
{
    public static class ParentsAddEditDelete
    {
        internal static void DeleteAParent(Person movareth, Person person)
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

        internal static void AddParent(Person movareth , Person newPerson)
        {
            if (movareth.Parents?.Count(w=>w.Gender == newPerson.Gender) > 0 )
            {
                Console.WriteLine("والد یا والده قبلا اضافه شده است. امکان افزودن والد دیگر از این جنس میسر نیست");
                return;
            }

            newPerson.MustBeLastNode = true;
            newPerson.SubNodeType = SubNodeType.Parents;
            newPerson.Tabagheh = TabaghehType.Tabagheh1;
            newPerson.Darajeh = 1;

            if (movareth.Parents == null)
                movareth.Parents = new List<Person>();
                
            movareth.Parents?.Add(newPerson);
        }
    }
}