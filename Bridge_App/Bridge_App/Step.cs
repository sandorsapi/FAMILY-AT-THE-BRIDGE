using System.Collections.Generic;

namespace Bridge_App.Interface
{
    public class Step
    {
        public string way { get; set; }
        public string moved_1 { get; set; }
        public string moved_2 { get; set; }
        public long time_1 { get; set; }
        public long time_2 { get; set; }
        public long runTime { get; set; }
    }
}