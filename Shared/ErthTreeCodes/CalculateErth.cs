using System;
using System.Collections.Generic;
using System.Linq;

namespace T00.Shared.ErthTreeCodes
{
    public class CalculateErth
    {
        private Person _movareth = null;
        public IList<Person> ValidVaratheh { get; private set; }
        public TabaghehType TabaghehErthBar { get; private set; }
        public int DarajehErthBar { get; private set; }
        public CalculateErth(Person movareth)
        {
            _movareth = movareth;
            FillValidVaratheh();
        }


        /// <summary>
        /// وراث ارث بر را استخراج می کند
        /// </summary>
        private void FillValidVaratheh()
        {
            ValidVaratheh = new List<Person>();

            TabaghehErthBar = DetermineTabaghehErthBar();

            switch (TabaghehErthBar)
            {
                case TabaghehType.Tabagheh1:
                    Console.WriteLine("وراث از طبقه اول هستند");
                    AddVorrath(ValidVaratheh, TabaghehType.Tabagheh1);
                    break;
                case TabaghehType.Tabagheh2:
                    Console.WriteLine("وراث از طبقه دوم هستند");
                    break;
                case TabaghehType.Tabagheh3:
                    Console.WriteLine("وراث از طبقه سوم هستند");
                    break;
                default:
                    Console.WriteLine("وارث زنده ای در هیچ کدام از طبقات سه گانه نیست");
                    break;
            }

        }

        /// <summary>
        /// تعیین طبقه وراث ارث بر
        /// </summary>
        /// <returns>طبقه ارث بر</returns>
        private TabaghehType DetermineTabaghehErthBar()
        {
            if (
                _movareth.Parents.Any(p => p.IsAlive && p.Gender != Gender.Neutral) ||
                AllChildrenWithAnyDarajeh(_movareth).Any(p => p.IsAlive)
               )
            {
                return TabaghehType.Tabagheh1; // ارث برها از طبقه اولند
            }

            if (
                AllGrandparentsWithAnyDarajeh(_movareth).Any(p => p.IsAlive) ||
                AllBrothersAndSistersWithAnyDarajeh(_movareth).Any(p => p.IsAlive)
               )
            {
                return TabaghehType.Tabagheh2; // ارث برها از طبقه دومند
            }

            if (
                AllAmamWithAnyDarajeh(_movareth).Any(p => p.IsAlive) ||
                AllAkhvalWithAnyDarajeh(_movareth).Any(p => p.IsAlive)
               )
            {
                return TabaghehType.Tabagheh3; // ارث برها از طبقه سومند
            }

            return TabaghehType.Unknown; //هیچ وارث زنده ای از هیچ طبقه ای وجود ندارد
        }

        #region Tabagheh 1
        /// <summary>
        /// همه فرزندان یا نوه های
        /// طبقه اول
        /// مورث صرفنظر از درجه آنها
        /// و زنده یا مرده بودنشان
        /// </summary>
        /// <param name="movareth"></param>
        /// <returns></returns>
        private IList<Person> AllChildrenWithAnyDarajeh(Person movareth)
        {
            var result = new List<Person>();
            foreach (var child in movareth.Children)
            {
                result.Add(child);
                FindChildrenOfAPerson(result, child);
            }

            return result;
        }

        /// <summary>
        /// استخراج همه فرزندان 
        /// و فرزندان فرزندان 
        /// یک نفر تا آخر
        /// صرفنظر درجه طبقه یا زنده یا مرده بودنشان
        /// </summary>
        /// <param name="result">لیستی که افراد به آن اضافه می شوند</param>
        /// <param name="person">شخصی که فرزندانش جستجو می شوند</param>
        private void FindChildrenOfAPerson(IList<Person> result, Person person)
        {
            if (person.Children == null)
                return;
        
            foreach (var child in person.Children)
            {
                result.Add(child);
                FindChildrenOfAPerson(result, child);
            }
        }

        #endregion //--End of Tabagheh 1

        #region  Tabagheh 2

                /// <summary>
        /// همه اجداد یا والدین آنها
        /// به هر درجه زنده یا مرده
        /// </summary>
        /// <param name="movareth">مورث</param>
        /// <returns>لیست اجداد و والدینشان</returns>
        private IList<Person> AllGrandparentsWithAnyDarajeh(Person movareth)
        {                
            var result = new List<Person>();            

            if (movareth.Ajdad == null)
                return result;
            
            foreach (var parent in movareth.Ajdad)
            {
                result.Add(parent);
                FindParentsOfAPerson(result, parent);
            }

            return result;
        }

        /// <summary>
        /// والدین اجداد را تا آخر می باید
        /// دقت: منظور از پرنت در اینجا والد درختی نیست
        /// بلکه والد ارثی در طبقه دوم و اجداد است
        /// </summary>
        /// <param name="result">لیست والدین اجداد</param>
        /// <param name="person">جدی که والدینش جستجو می شوند</param>
        private void FindParentsOfAPerson(IList<Person> result, Person person)
        {
            foreach (var parent in person.Parents)
            {
                result.Add(parent);
                FindParentsOfAPerson(result, parent);
            }
        }



        /// <summary>
        /// همه برادران و خواهران و همه فرزندان و نوادگان آنها
        /// از هر درجه و نوعی
        /// </summary>
        /// <param name="movareth">مورث</param>
        /// <returns>همه بردادران و خواهران و نسلشان</returns>
        private IList<Person> AllBrothersAndSistersWithAnyDarajeh(Person movareth)
        {
            var result = new List<Person>();
            if (movareth.BrothersAndSisters == null)
                return result;

            foreach (var child in movareth.BrothersAndSisters)
            {
                result.Add(child);
                FindChildrenOfAPerson(result, child);
            }

            return result;
        }


        #endregion //-- Tabagheh 2

        #region Tabagheh 3
        /// <summary>
        /// همه اعمام 
        /// و فرزندان و نوادگان آنها صرفنظر از
        /// زنده یا مرده بودن و نوع آنها
        /// </summary>
        /// <param name="movareth">مورث</param>
        /// <returns>لیست اعمام از هر درجه و نوع</returns>
        private IList<Person> AllAmamWithAnyDarajeh(Person movareth)
        {
            var result = new List<Person>();
            if (movareth.Amam == null)
                return result;

            foreach (var child in movareth.Amam)
            {
                result.Add(child);
                FindChildrenOfAPerson(result, child);
            }

            return result;
        }

        /// <summary>
        /// همه اخوال و فرزندان و نوادگان آنها
        /// صرفنظر از درجه زنده یا مرده بودن و نوعشان
        /// </summary>
        /// <param name="movareth">مورث</param>
        /// <returns>لیست همه اخوال</returns>
        private IList<Person> AllAkhvalWithAnyDarajeh(Person movareth)
        {
            var result = new List<Person>();
            if (movareth.Akhval == null)
                return result;

            foreach (var child in movareth.Akhval)
            {
                result.Add(child);
                FindChildrenOfAPerson(result, child);
            }

            return result;
        }
        #endregion //-- Tabagheh 3

        void AddVorrath(IList<Person> vorrath, TabaghehType tabaghehType)
        {
            switch (tabaghehType)
            {
                case TabaghehType.Tabagheh1:
                    
                    // Parents
                    foreach (var vareth in _movareth.Parents.Where(p=>p.IsAlive && p.Gender != Gender.Neutral))
                    {
                        vorrath.Add(vareth);
                    }

                    // Children
                    var allChildren = AllChildrenWithAnyDarajeh(_movareth);

                    //کمترین درجه فرزندان یا نوه های زنده را بیاب
                    // هر فرزند یا نوه با درجه بالاتر ارث بر نخواهد بود
                    var aliveChildren = allChildren.Where(p=>p.IsAlive);
                    if (aliveChildren.Count() > 0)
                    {
                        var minDarajeh = aliveChildren.Min(p => p.Darajeh);
                        allChildren.Where(p=>p.IsAlive && p.Darajeh == minDarajeh).ToList().ForEach(p => vorrath.Add(p));
                    }
                    
                    break;
                case TabaghehType.Tabagheh2:
                    Console.WriteLine("تغیین وراث طبقه ۲ هنوز نوشته نشده است");
                    break;
                case TabaghehType.Tabagheh3:
                    Console.WriteLine("تغیین وراث طبقه 3 هنوز نوشته نشده است");
                    break;

            }
        }
    }
}