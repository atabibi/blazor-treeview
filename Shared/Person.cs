using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace T00.Shared
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

        public List<Person> Children { get; set; }

        public List<Person> Parents { get; set; }

        public List<Person> Hamsar { get; set; }

        public List<Person> Ajdad { get; set; }

        public List<Person> BrothersAndSisters { get; set; }
        public List<Person> Amam { get; set; }
        public List<Person> Akhval { get; set; }

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
                int id = (int) this.Tabagheh & 0x000000FF | (int) this.SubNodeType & 0x0000FF00 |
                         this.Darajeh & 0x00FF0000;
                return id;
            }
        }

        public static TabaghehType GetTabaghehFromId(int id)
        {
            return (TabaghehType) (id & 0x000000FF);
        }

        public static SubNodeType GetSubNodeTypeFromId(int id)
        {
            return (SubNodeType)(id & 0x0000FF00);
        }

        public static int GetDarajehFromId(int id)
        {
            return (id & 0x00FF0000);
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