using System;
using System.Collections.Generic;
using System.Text;

namespace Sideline.Loadtest.viewmodels
{
    public class Joinmatch
    {
        public int id { get; set; }
        public int datum { get; set; }
        public int duration { get; set; }
        public int mode { get; set; }
        public string start { get; set; }
        public Teaminfo TeamInfo0 { get; set; }
        public Teaminfo TeamInfo1 { get; set; }
    }
}
