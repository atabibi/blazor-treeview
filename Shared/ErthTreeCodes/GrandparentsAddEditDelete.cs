using System;
using System.Collections.Generic;
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


        /// <summary>
        /// افزودن یک جد یا جده ی درجه ۱
        /// </summary>
        /// <param name="movareth">مورث</param>
        /// <param name="newPerson">جد یا جده</param>
        internal static void AddGrandparent(Person movareth, Person newPerson)
        {
            if (newPerson.AbiOmmi == AbiOmmi.Abavaini)
            {
                Console.WriteLine("جد و جده نمی توانند ابوینی باشند");
                return;
            }

            if (movareth.Ajdad == null)
            {
                movareth.Ajdad = new List<Person>();
            }

            if (movareth.Ajdad.Count(a => a.AbiOmmi == newPerson.AbiOmmi && a.Gender == newPerson.Gender) > 0)
            {
                // قبلا اضافه شده است;
                Console.WriteLine("این جد یا جده قبلا افزوده شده است");
                return;
            }

            
            newPerson.Darajeh = 1;
            newPerson.Tabagheh = TabaghehType.Tabagheh2;
            newPerson.SubNodeType = SubNodeType.Grandparents;
            
            movareth.Ajdad.Add(newPerson);
        }


        /// <summary>
        /// افزودن یک جد یا جده درجه ۲ یا بالاتر
        /// </summary>
        /// <param name="person">جد فرزند</param>
        /// <param name="newPerson">جد جدید</param>
        internal static void AddParent(Person person, Person newPerson)
        {
            newPerson.SubNodeType = SubNodeType.Grandparents;
            newPerson.Tabagheh = TabaghehType.Tabagheh2;
            newPerson.Darajeh = person.Darajeh + 1;
            newPerson.AbiOmmi = person.AbiOmmi;

            if (person.Parents == null)
            {
                person.Parents = new List<Person>();
            }

            if (person.Parents?.Count(p => p.Gender == newPerson.Gender) == 0)
            {                
                person.Parents.Add(newPerson);
            }
        }

    }
}