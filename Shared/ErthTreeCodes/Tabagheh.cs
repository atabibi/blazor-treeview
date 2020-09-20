using System.Collections.Generic;

namespace T00.Shared.ErthTreeCodes
{
    public class Tabagheh
    {
        public TabaghehType Type { get; set; }
        public string Title { get; set; }

        public IList<SubNode> SubNodes { get; set; }
    }

    public enum TabaghehType {
        Tabagheh1,
        Tabagheh2,
        Tabagheh3,
        Hamsar,
        Unknown = 255
    }

    public class SubNode
    {
        public SubNodeType Type { get; set; }
        public string Title { get; set; }
    }

    public enum SubNodeType {
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