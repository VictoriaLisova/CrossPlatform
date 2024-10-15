using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Trip
    {
        public int StartVillageNumber { get; set; }
        public int StartTime { get; set; }
        public int EndVillageNumber { get; set; }
        public int EndTime { get; set; }

        public Trip(int startVillage, int startTime, int endVillage, int endTime)
        {
            StartVillageNumber = startVillage;
            StartTime = startTime;
            EndVillageNumber = endVillage;
            EndTime = endTime;
        }

    }
}
