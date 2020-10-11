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
}