using System;

namespace T00.Shared.ErthTreeCodes
{
    public static class HamsarAddEditDelete
    {
        public static void DeleteHamsar(Person movareth, Person person)
        {
            if (movareth.Hamsar.Remove(person))
            {
                Console.WriteLine($"{person.FullName} حذف شد");
            }
            else
            {
                Console.WriteLine($"{person.FullName} یافت نشد");
            }
        }
    }
}