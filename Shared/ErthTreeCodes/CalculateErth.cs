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
            CalcErths();
        }

        private void CalcErths()
        {
            // ابتدا طبقه ارث بر را مشخص کن
            TabaghehErthBar = DetermineTabaghehErthBar();

            switch (TabaghehErthBar)
            {
                case TabaghehType.Tabagheh1:
                    CalcErthTabagheh1();
                    break;
                case TabaghehType.Tabagheh2:
                    CalcErthTabagheh2();
                    break;
                case TabaghehType.Tabagheh3:
                    CalcErthTabagheh3();
                    break;
                
            }
        }

        private void CalcErthTabagheh1()
        {
            Console.WriteLine("ارث طبقه اول هنوز کامل نشد ه است");
            
            // مرحله اول: حذف شاخه هایی که در انتهای آنها گره زنده وجود ندارد
            #if DEBUG
            Console.WriteLine("Before RemoveEndlessRoots");
            ShowChildrenInConsole(_movareth.Children);
            #endif
            _movareth.Children = RemoveEndlessRoots(_movareth.Children);
            #if DEBUG
            Console.WriteLine("After RemoveEndlessRoots");
            ShowChildrenInConsole(_movareth.Children);
            #endif
        }

        private void CalcErthTabagheh2()
        {
            Console.WriteLine(" ارث طبقه دوم را هنوز ننوشته ام");
        }

        private List<Person> RemoveEndlessRoots(List<Person> children)
        {
            for (int i = children.Count-1; i >=0; i--)
            {
                if (children[i].Children.Count > 0)
                    children[i].Children = RemoveEndlessRoots(children[i].Children);

                if (!(children[i].IsAlive) && children[i].Children.Count == 0) 
                {
                    // گره زنده نیست و هیچ فرزندی هم ندارد
                    // بنابراین باید حذف شود
                    children.RemoveAt(i);
                }
            }

            return children;
        }

        private void CalcErthTabagheh3()
        {
            Console.WriteLine(" ارث طبقه سوم را هنوز ننوشته ام");
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
                    AddVorrath(ValidVaratheh, TabaghehType.Tabagheh2);
                    break;
                case TabaghehType.Tabagheh3:
                    Console.WriteLine("وراث از طبقه سوم هستند");
                    AddVorrath(ValidVaratheh, TabaghehType.Tabagheh3);
                    break;
                default:
                    Console.WriteLine("وارث زنده ای در هیچ کدام از طبقات سه گانه نیست");
                    break;
            }

            AddHamsar(ValidVaratheh);

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
                    AddErthBarParents(vorrath);

                    // Children
                    AddErthBarChildrenWithAnyDarajeh(vorrath);
                    
                    break;
                case TabaghehType.Tabagheh2:
                
                    // اجداد
                    AddErthBarAjdad(vorrath);

                    // برادران و خواهران
                    AddErthBarBrothersAndSisters(vorrath);

                    break;
                case TabaghehType.Tabagheh3:
                    // اعمام
                    AddAmam(vorrath);

                    // اخوال
                    AddAkhval(vorrath);

                    break;
                case TabaghehType.Hamsar:
                    Console.WriteLine("افزودن همسر هنوز نوشته نشده است");
                    break;

            }
        }

        #region افزودن طبقه اول به لیست وراث

        /// <summary>
        /// افزودن پدر یا مادر یا هر دو
        /// به لیست وراث در صورت 
        /// زنده بودن
        /// </summary>
        /// <param name="vorrath"></param>
        private void AddErthBarParents(IList<Person> vorrath)
        {
            foreach (var vareth in _movareth.Parents.Where(p=>p.IsAlive && p.Gender != Gender.Neutral))
            {
                vorrath.Add(vareth);
            }
        }

        /// <summary>
        /// افزودن فرزندان یا نوه های
        /// ارث بر به لیست وراث
        /// </summary>
        /// <param name="vorrath"></param>
        private void AddErthBarChildrenWithAnyDarajeh(IList<Person> vorrath)
        {
            var allChildren = AllChildrenWithAnyDarajeh(_movareth);

            //کمترین درجه فرزندان یا نوه های زنده را بیاب
            // هر فرزند یا نوه با درجه بالاتر ارث بر نخواهد بود
            var aliveChildren = allChildren.Where(p=>p.IsAlive);
            if (aliveChildren.Count() > 0)
            {
                var minDarajeh = aliveChildren.Min(p => p.Darajeh);
                allChildren.Where(p=>p.IsAlive && p.Darajeh == minDarajeh).ToList().ForEach(p => vorrath.Add(p));
            }
        }

        #endregion // افزودن طبقه اول به لیست وراث

        #region افزودن طبقه دوم به لیست وراث


        /// <summary>
        /// افزودن اجداد ارث بر ا
        /// از هر درجه ای
        /// </summary>
        /// <param name="vorrath"></param>
        private void AddErthBarAjdad(IList<Person> vorrath)
        {
            var allAjdad = AllGrandparentsWithAnyDarajeh(_movareth);

            // کمترین درجه اجداد زنده را بیاب
            // هر جد یا جده با درجه بالاتر ارث  بر نخواهد بود
            var aliveAjdad = allAjdad.Where(p =>p.IsAlive);
            if (aliveAjdad.Count() > 0)
            {
                var minDarajeh = aliveAjdad.Min(p => p.Darajeh);
                allAjdad.Where(p=>p.IsAlive && p.Darajeh == minDarajeh).ToList().ForEach(p => vorrath.Add(p));
            }
        }


        /// <summary>
        /// افزودن برادران و یا خواهران ارث بر
        /// از هر درجه و نوعی
        /// </summary>
        /// <param name="vorrath"></param>
        private void AddErthBarBrothersAndSisters(IList<Person> vorrath)
        {
            var allBrothersAndSisters = AllBrothersAndSistersWithAnyDarajeh(_movareth);

            // فقط برادران و خواهران زند را بیاب
            var aliveBS = allBrothersAndSisters.Where(p => p.IsAlive);

            // حذف کلاله ابی در صورت زنده بودن حتی یک کلاله ابوینی
            bool anyAbavaini = aliveBS.Count(p => p.AbiOmmi == AbiOmmi.Abavaini) > 0;
            if (anyAbavaini)
            {
                aliveBS = aliveBS.Where(p => p.AbiOmmi != AbiOmmi.Abi);
            }

            // نگهداشتن پایین ترین درجه و حذف همه درجات بالاتر
            if (aliveBS.Count() > 0)
            {
                var minDarajeh = aliveBS.Min(p => p.Darajeh);
                aliveBS.Where(p => p.Darajeh == minDarajeh).ToList().ForEach(p => vorrath.Add(p));
            }
        }

        #endregion
    
        #region  افزودن طبقه سوم به لیست وراث

        /// <summary>
        /// افزودن اعمام ارث برا
        /// از هر درجه و نوعی
        /// </summary>
        /// <param name="vorrath"></param>
        private void AddAmam(IList<Person> vorrath)
        {
            // همه اعمام از هر درجه ای زنده یا مرده
            var allAmam = AllAmamWithAnyDarajeh(_movareth);

            // فقط اعمام زنده
            var aliveAmam = allAmam.Where(p => p.IsAlive);

            // حذف کلاله ابی در صورت زنده بودن حتی یک کلاله ابوینی
            bool anyAbavaini = aliveAmam.Count(p => p.AbiOmmi == AbiOmmi.Abavaini) > 0;
            if (anyAbavaini)
            {
                aliveAmam = aliveAmam.Where(p => p.AbiOmmi != AbiOmmi.Abi);
            }

            // نگهداشتن پایین ترین درجه و حذف همه درجات بالاتر
            if (aliveAmam.Count() > 0)
            {
                var minDarajeh = aliveAmam.Min(p => p.Darajeh);
                aliveAmam.Where(p => p.Darajeh == minDarajeh).ToList().ForEach(p => vorrath.Add(p));
            }
            
        }

        /// <summary>
        /// افزدون اخوال ارث بر
        /// از هر درجه و نوعی
        /// </summary>
        /// <param name="vorrath"></param>
        private void AddAkhval(IList<Person> vorrath)
        {
            // همه اخوال از هر درجه ای زنده یا مرده
            var allAkhval = AllAkhvalWithAnyDarajeh(_movareth);

            // فقط اخوال زنده
            var aliveAkhval = allAkhval.Where(p => p.IsAlive);

            // حذف کلاله ابی در صورت زنده بودن حتی یک کلاله ابوینی
            bool anyAbavaini = aliveAkhval.Count(p => p.AbiOmmi == AbiOmmi.Abavaini) > 0;
            if (anyAbavaini)
            {
                aliveAkhval = aliveAkhval.Where(p => p.AbiOmmi != AbiOmmi.Abi);
            }

            // نگهداشتن پایین ترین درجه و حذف همه درجات بالاتر
            if (aliveAkhval.Count() > 0)
            {
                var minDarajeh = aliveAkhval.Min(p => p.Darajeh);
                aliveAkhval.Where(p => p.Darajeh == minDarajeh).ToList().ForEach(p => vorrath.Add(p));
            }
        }

        #endregion

        #region افزودن همسر(ان) به لیست وراث
        
        private void AddHamsar(IList<Person> vorrath)
        {
            _movareth.Hamsar.Where(p => p.IsAlive).ToList().ForEach(p => vorrath.Add(p));
        }
        #endregion
    

        #if DEBUG
        void ShowChildrenInConsole(List<Person> children,string strLine="")
        {
            strLine += "-";
            foreach (var p in children)
            {
                Console.WriteLine(strLine + p.FullName);
                ShowChildrenInConsole(p.Children, strLine);
            }
        }
        #endif
    }
}