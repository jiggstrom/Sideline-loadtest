using System;
using System.Collections.Generic;
using System.Text;

namespace Sideline.Loadtest.viewmodels
{

    public class Matchstate
    {
        public int MatchTimeInSeconds { get; set; }
        public Playerinfo PlayerInformation { get; set; }
        public Slot Slot0 { get; set; }
        public Slot Slot1 { get; set; }
        public Slot Slot2 { get; set; }
        public Slot Slot3 { get; set; }
        public Slot Slot4 { get; set; }
        public Slot Slot5 { get; set; }
        public Slot Slot6 { get; set; }
        public Slot Slot7 { get; set; }
        public Slot Slot8 { get; set; }
        public Slot Slot9 { get; set; }
        public Slot Slot10 { get; set; }
        public Slot Slot11 { get; set; }
        public Slot Slot12 { get; set; }
        public Slot Slot13 { get; set; }
        public Slot Slot14 { get; set; }
        public Slot Slot15 { get; set; }
        public Slot Slot16 { get; set; }
        public Slot Slot17 { get; set; }
        public Slot Slot18 { get; set; }
        public Slot Slot19 { get; set; }
        public Slot Slot20 { get; set; }
        public Slot Slot21 { get; set; }
        public Slot Slot22 { get; set; }
        public Slot Slot23 { get; set; }
        public Slot Slot24 { get; set; }
        public Slot Slot25 { get; set; }
        public Slot Slot26 { get; set; }
        public Slot Slot27 { get; set; }
        public Slot Slot28 { get; set; }
        public Slot Slot29 { get; set; }
        public Slot Slot30 { get; set; }
        public Slot Slot31 { get; set; }
    }

    public class Slot
    {
        public float Balance { get; set; }
        public int Flags { get; set; }
        public Top Top0_0 { get; set; }
        public Top Top0_1 { get; set; }
        public Top Top0_2 { get; set; }
        public Top Top1_0 { get; set; }
        public Top Top1_1 { get; set; }
        public Top Top1_2 { get; set; }
        public object Result { get; set; }
    }

    public class Top
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int Influence { get; set; }
        public string ProfileImg { get; set; }
        public int SSA { get; set; }
        public int TeamId { get; set; }
    }

}
