using System;
using System.Collections.Generic;
using System.Linq;

namespace T00.Shared.ErthTreeCodes
{
    public static class BrothersSistersAddEditDelete
    {
        public static void DeleteABrotherOrSister(Person movareth, Person person)
        {
            var personParentFound = FindABrotherOrSisterParent(movareth, person);

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

        /// <summary>
        /// افزودن یک برادر یا خواهر به مورث
        /// نکته: این تابع صرفا برادران و خواهران درجه ۱ را اضافه می کند.
        /// برای زیرشاخه ها از تابع 
        /// AddNodeToPerson
        /// در کلاس
        /// AddNode
        /// استفاده کنید
        /// </summary>
        /// <param name="movareth">مورث</param>
        /// <param name="bsPerson">برادر یا خواهر جدید</param>
        /// <param name="subNodeType">نوع برادر خواهر ابی یا ابوینی</param>
        internal static void AddABrotherOrSister(Person movareth, Person bsPerson, SubNodeType subNodeType)
        {
            bsPerson.SubNodeType = subNodeType;
            bsPerson.Tabagheh = TabaghehType.Tabagheh2;
            bsPerson.Darajeh = 1;

            if (
                (subNodeType != SubNodeType.BrothersAndSistersAbavaini) &&
                (subNodeType != SubNodeType.BrothersAndSistersAbi) &&
                (subNodeType != SubNodeType.BrothersAndSistersOmmi)
             )
            {
                Console.WriteLine("خطا! تنها برادران و خواهران درجه یک اجازه افزوده شدن دارند");
            }
            
            if (movareth.BrothersAndSisters == null)
                movareth.BrothersAndSisters = new List<Person>();

            movareth.BrothersAndSisters.Add(bsPerson);
        }

        static Person FindABrotherOrSisterParent(Person parent, Person bsOrHisChild)
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
                        parentFound = FindABrotherOrSisterParent(p, bsOrHisChild);
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
                        parentFound = FindABrotherOrSisterParent(p, bsOrHisChild);
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