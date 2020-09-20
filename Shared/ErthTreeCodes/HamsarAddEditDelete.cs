using System;
using System.Collections.Generic;

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

        public static void AddHamsar(Person movareth, Person newPerson)
        {
            if (movareth.Gender == Gender.Male && movareth.Hamsar?.Count > 3)
            {
                Console.WriteLine("حداکثر امکان افزودن ۴ زوجه وجود دارد");
                return;
            }

            if (movareth.Gender == Gender.Female && movareth.Hamsar?.Count > 0)
            {
                Console.WriteLine("حداکثر امکان افزودن ۱ زوج وجود دارد");
                return;
            }

            if (movareth.Hamsar == null)
            {
                movareth.Hamsar = new List<Person>();
            }

            newPerson.Gender = movareth.Gender == Gender.Male ? Gender.Female : Gender.Male;
            newPerson.MustBeLastNode = true;
            newPerson.Tabagheh = TabaghehType.Hamsar;

            movareth.Hamsar.Add(newPerson);
        }

    }
}