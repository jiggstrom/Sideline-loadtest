using System;
using System.Collections.Generic;
using System.Text;

namespace Sideline.Loadtest.viewmodels
{
    public class Stats
    {
        public Playerinfo PlayerInfo { get; set; }
        public Teaminfo TeamInfo { get; set; }
        public Ligainfo LigaInfo { get; set; }
        public Ligateaminfo LigaTeamInfo { get; set; }
        public Totalstats TotalStats { get; set; }
        public object StatsRanking { get; set; }
        public Seasonstats SeasonStats0 { get; set; }
        public Seasonstats SeasonStats1 { get; set; }
        public object ShowProfile { get; set; }
    }

    public class Teaminfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Color0 { get; set; }
        public string Color1 { get; set; }
        public int Liga { get; set; }
        public int Conf { get; set; }
        public int Div { get; set; }
        public int Stars { get; set; }
        public int CoachId { get; set; }
        public int AssCoach1Id { get; set; }
        public int AssCoach2Id { get; set; }
        public string Arena { get; set; }
        public int SecondsFromUTC { get; set; }
    }

    public class Ligainfo
    {
        public int LigaId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }

    public class Ligateaminfo
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Matches { get; set; }
    }

    public class Totalstats
    {
        public int Datum { get; set; }
        public int TotMatches { get; set; }
        public int Impact { get; set; }
        public int PassYds { get; set; }
        public int RushYds { get; set; }
        public int RecvYds { get; set; }
        public int TotYds { get; set; }
        public int Comp { get; set; }
        public int TD { get; set; }
        public int PassBlock { get; set; }
        public int RunBlock { get; set; }
        public int Car { get; set; }
        public int FGM { get; set; }
        public int Point { get; set; }
        public int TacSol { get; set; }
        public int TacAst { get; set; }
        public int TacTot { get; set; }
        public int Sck { get; set; }
        public int Hurr { get; set; }
        public int TacFoLo { get; set; }
        public int Inter { get; set; }
        public int PDef { get; set; }
        public int SFTY { get; set; }
        public int PowHit { get; set; }
        public int FPunt { get; set; }
        public int Fum { get; set; }
        public int RushComp { get; set; }
    }

    public class Seasonstats
    {
        public int Datum { get; set; }
        public int TotMatches { get; set; }
        public int Impact { get; set; }
        public int PassYds { get; set; }
        public int RushYds { get; set; }
        public int RecvYds { get; set; }
        public int TotYds { get; set; }
        public int Comp { get; set; }
        public int TD { get; set; }
        public int PassBlock { get; set; }
        public int RunBlock { get; set; }
        public int Car { get; set; }
        public int FGM { get; set; }
        public int Point { get; set; }
        public int TacSol { get; set; }
        public int TacAst { get; set; }
        public int TacTot { get; set; }
        public int Sck { get; set; }
        public int Hurr { get; set; }
        public int TacFoLo { get; set; }
        public int Inter { get; set; }
        public int PDef { get; set; }
        public int SFTY { get; set; }
        public int PowHit { get; set; }
        public int FPunt { get; set; }
        public int Fum { get; set; }
        public int RushComp { get; set; }
    }

}
