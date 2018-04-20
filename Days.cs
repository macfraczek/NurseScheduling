using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseScheduling
{
    public class Days
    {
        public int[] Early = new int[3];
        public int[] Day = new int[3];
        public int[] Late = new int[3];
        public int Night;
    }

    public class DaysWeek : Days
    {
        public new int[] Early = new int[3];
        public int[] Day = new int[3];
        public int[] Late = new int[3];
        public int[] Night = new int[1];
    }
    public class DaysWeekEnd : Days
    {
        public int[] Early = new int[2];
        public int[] Day = new int[2];
        public int[] Late = new int[2];
        public int[] Night = new int[1];
    }
}
