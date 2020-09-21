using System;
using System.Collections.Generic;
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

        /// <summary>
        /// افزودن یک عمو عمه دایی یا خاله
        /// از هریک از انواع ابوینی ابی یا امی
        /// فقط درجه یک به مورث
        /// نکته: این تابع تنها اعمام و اخوال درجه یک را اضافه می کند 
        /// برای درجات بالاتر از تابع
        /// AddNodeToPerson
        /// در کلاس 
        /// AddNodes
        /// استفاده کنید
        /// </summary>
        /// <param name="movareth">مورث</param>
        /// <param name="amAkhPerson">عمو عمه خاله دایی درجه یک</param>
        /// <param name="subNodeType">نوع عمو دایی خاله عمه ابی ابوینی </param>
        internal static void AddAmmOrAkh(Person movareth, Person amAkhPerson, SubNodeType subNodeType)
        {
            amAkhPerson.SubNodeType = subNodeType;
            amAkhPerson.Darajeh = 1;
            amAkhPerson.Tabagheh = TabaghehType.Tabagheh3;

            if (
                (subNodeType == SubNodeType.AkhvalAbavaini) ||
                (subNodeType == SubNodeType.AkhvalAbi) ||
                (subNodeType == SubNodeType.AkhvalOmmi) 
            )
            {
                if (movareth.Akhval == null)
                    movareth.Akhval = new List<Person>();

                movareth.Akhval.Add(amAkhPerson);
            }
            else if (
                (subNodeType == SubNodeType.AmamAbavaini) ||
                (subNodeType == SubNodeType.AmamAbi) ||
                (subNodeType == SubNodeType.AmamOmmi)
            )
            {
                if (movareth.Amam == null)
                    movareth.Amam = new List<Person>();

                movareth.Amam.Add(amAkhPerson);
            }
            else
            {
                Console.WriteLine("خطا! تنها اعمام و اخوال اجازه افزوده شدن دارند");
                return;
            }
        }
    }
}