using System.Collections.Generic;

namespace T00.Shared.ErthTreeCodes
{
    public class Tabagheh
    {
        public TabaghehType Type { get; set; }
        public string Title { get; set; }

        public IList<SubNode> SubNodes { get; set; }
    }

    public enum TabaghehType
    {
        Tabagheh1,
        Tabagheh2,
        Tabagheh3,
        Hamsar,
        Unknown = 255
    }

    public static class TabaghehTypeExtensions
    {
        public static string ToPersianString(this TabaghehType tabaghehType)
        {
            switch (tabaghehType)
            {
                case TabaghehType.Hamsar:
                    return "همسر";
                case TabaghehType.Tabagheh1:
                    return "طبقه اول";
                case TabaghehType.Tabagheh2:
                    return "طبقه دوم";
                case TabaghehType.Tabagheh3:
                    return "طبقه سوم";
                case TabaghehType.Unknown:
                    return "طبقه نامشخص!";
                default:
                    return "نامشخص";
            }
        }
    }

    public class SubNode
    {
        public SubNodeType Type { get; set; }
        public string Title { get; set; }
    }

    public enum SubNodeType
    {
        Parents,
        Children,
        Grandparents,
        BrothersAndSistersAbavaini,
        BrothersAndSistersAbi,
        BrothersAndSistersOmmi,
        AmamAbavaini,
        AmamAbi,
        AmamOmmi,
        AkhvalAbavaini,
        AkhvalAbi,
        AkhvalOmmi,
        Unknown = 255
    }

    public static class SubNodeTypeExtensions
    {
        public static string ToPersianString(this SubNodeType subNodeType, bool isMale, int darajeh)
        {
            string zadeh = darajeh <= 1 ? "" : " زاده";
            switch (subNodeType)
            {
                case SubNodeType.AkhvalAbavaini:
                    return isMale ? "دایی" : "خاله" + zadeh + " ابوینی";
                case SubNodeType.AkhvalAbi:
                    return isMale ? "دایی" : "خاله" + zadeh + " ابی";
                case SubNodeType.AkhvalOmmi:
                    return isMale ? "دایی" : "خاله" + zadeh + " امی";
                case SubNodeType.AmamAbavaini:
                    return isMale ? "عمو" : "عمه" + zadeh + " ابوینی";
                case SubNodeType.AmamAbi:
                    return isMale ? "عمو" : "عمه" + zadeh + " ابی";
                case SubNodeType.AmamOmmi:
                    return isMale ? "عمو" : "عمه" + zadeh + " امی";
                case SubNodeType.BrothersAndSistersAbavaini:
                    return isMale ? "برادر" : "خواهر" + zadeh + " ابوینی";
                case SubNodeType.BrothersAndSistersAbi:
                    return isMale ? "برادر" : "خواهر" + zadeh + " ابی";
                case SubNodeType.BrothersAndSistersOmmi:
                    return isMale ? "برادر" : "خواهر" + zadeh + " امی";
                case SubNodeType.Children:
                    return darajeh == 1 ? (isMale ? "پسر" : "دختر") : "نوه";
                case SubNodeType.Grandparents:
                    return isMale ? "جد" : "جده";
                case SubNodeType.Parents:
                    return isMale ? "پدر" : "مادر";
                case SubNodeType.Unknown:
                    return "نامشخص";
                default:
                    return  "خطا! نسبت خویشاوندی تعریف نشده است";
            }
        }
    }
}