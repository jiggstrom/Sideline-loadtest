using System;
using System.Collections.Generic;
using System.Text;

namespace Sideline.Loadtest.viewmodels
{

    public class Playerinfo
    {
        public Publicinfo PublicInfo { get; set; }
        public Privateinfo PrivateInfo { get; set; }
    }

    public class Publicinfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Number { get; set; }
        public int TeamId { get; set; }
        public object Role { get; set; }
        public object OffRole { get; set; }
        public object DefRole { get; set; }
        public object SecRole { get; set; }
        public int Stars { get; set; }
        public string Nationality { get; set; }
        public int Level { get; set; }
        public string ProfileImgUrl { get; set; }
    }

    public class Privateinfo
    {
        public int Influence { get; set; }
        public int Currency { get; set; }
        public int Gold { get; set; }
        public int Energy { get; set; }
        public int Tp { get; set; }
        public int Salary { get; set; }
        public int CurrencyCollect { get; set; }
        public int GoldCollect { get; set; }
        public int TPCollect { get; set; }
        public int Flags { get; set; }
        public int SSU { get; set; }
    }

}
