using System;

namespace InteractiveCovidMap.Models
{
    public class Status
    {
        public DateTime buildTime { get; set; }
        public bool production { get; set; }
        public int runNumber { get; set; }
    }
}