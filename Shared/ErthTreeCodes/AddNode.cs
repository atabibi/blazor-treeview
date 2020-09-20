using System;
using System.Collections.Generic;

namespace T00.Shared.ErthTreeCodes
{
    //Todo: Must be complete!
    public static class AddNode
    {
        /// <summary>
        /// افزودن گره ی جدید به شاخه های ریشه و نه نوادگان:
        /// والدین
        /// فرزندان
        /// اجداد
        /// برادران و خواهران
        /// اعمام و اخوال
        /// </summary>
        /// <param name="subNode">گره ریشه</param>
        /// <param name="person">گره موجود</param>
        /// <param name="newPerson">گره جدید</param>
        public static void AddNodeToSubNode(SubNode subNode, Person person, Person newPerson)
        {
            switch (subNode.Type)
            {
                case SubNodeType.Parents:
                    ParentsAddEditDelete.AddParent(person, newPerson);
                    break;
                case SubNodeType.Children:
                    ChildrenAddEditDelete.AddChild(person, newPerson);
                    break;
                case SubNodeType.Grandparents:
                    GrandparentsAddEditDelete.AddGrandparent(person, newPerson);
                    break;
                case SubNodeType.BrothersAndSistersAbavaini:
                case SubNodeType.BrothersAndSistersAbi:
                case SubNodeType.BrothersAndSistersOmmi:
                    break;
                case SubNodeType.AmamAbavaini:
                case SubNodeType.AmamAbi:
                case SubNodeType.AmamOmmi:
                    break;
                case SubNodeType.AkhvalAbavaini:
                case SubNodeType.AkhvalAbi:
                case SubNodeType.AkhvalOmmi:
                    break;
            }
        }


        /// <summary>
        /// افزودن گره به گره موجود
        /// مثلا افزودن نوه به فرزند
        /// </summary>
        /// <param name="person">گره موجود</param>
        /// <param name="newPerson">گره جدید</param>
        public static void AddNodeToPerson(Person person, Person newPerson)
        {            
            if (person.SubNodeType == SubNodeType.Grandparents)
            {                
                GrandparentsAddEditDelete.AddParent(person, newPerson);
            }
            else
            {
                if (person.Children == null)
                {
                    person.Children = new List<Person>();
                }

                newPerson.Tabagheh = person.Tabagheh;
                newPerson.Darajeh = person.Darajeh + 1;
                newPerson.Martbeh = person.Martbeh;
                newPerson.SubNodeType = person.SubNodeType;
                newPerson.AbiOmmi = person.AbiOmmi;

                person.Children.Add(newPerson);

            }
        }
    }
}