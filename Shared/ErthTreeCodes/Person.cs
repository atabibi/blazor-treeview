﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace T00.Shared.ErthTreeCodes
{
    public class Person
    {
        public string FullName { get; set; }
        public Gender Gender { get; set; } = Gender.Male;

        public bool IsAlive { get; set; } = true;

        public TabaghehType Tabagheh { get; set; } = TabaghehType.Unknown;

        public SubNodeType SubNodeType { get; set; } = SubNodeType.Unknown;

        public int Darajeh { get; set; }

        public int Martbeh { get; set; }

        public AbiOmmi AbiOmmi { get; set; }

        public List<Person> Children { get; set; } = new List<Person>();

        public List<Person> Parents { get; set; } = new List<Person>();

        public List<Person> Hamsar { get; set; } = new List<Person>();

        public List<Person> Ajdad { get; set; } = new List<Person>();

        public List<Person> BrothersAndSisters { get; set; } = new List<Person>();
        public List<Person> Amam { get; set; } = new List<Person>();
        public List<Person> Akhval { get; set; } = new List<Person>();

        /// <summary>
        /// در مورد والدین و همسر نباید امکان
        /// افزودن گره جدید باشد.
        /// این فیلد بخاطر این اضافه شد
        /// </summary>
        public bool MustBeLastNode { get; set; } = false;


        public int Id
        {
            get
            {
                int id = (int)this.Tabagheh & 0x000000FF | (int)this.SubNodeType & 0x0000FF00 |
                         this.Darajeh & 0x00FF0000;
                return id;
            }
        }

        public static TabaghehType GetTabaghehFromId(int id)
        {
            return (TabaghehType)(id & 0x000000FF);
        }

        public static SubNodeType GetSubNodeTypeFromId(int id)
        {
            return (SubNodeType)(id & 0x0000FF00);
        }

        public static int GetDarajehFromId(int id)
        {
            return (id & 0x00FF0000);
        }

        public override string ToString()
        {
            string strZoj = this.Gender == Gender.Male ? "زوج" : "زوجه";

            return this.Tabagheh == TabaghehType.Hamsar ?
                 $"{this.FullName}: همسر ({strZoj})" :
                 $"{this.FullName} از {this.Tabagheh.ToPersianString()} و درجه {this.Darajeh} ({this.SubNodeType.ToPersianString(this.Gender == Gender.Male, this.Darajeh)})";
        }
    }

    public enum Gender
    {
        Neutral,
        Male,
        Female
    }

    public enum AbiOmmi
    {
        Abavaini,
        Abi,
        Ommi
    }
}