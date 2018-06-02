using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseScheduling
{
    static class SaveTo
    {

        internal static void SaveToCsv(Schedule sche, NurseList nurseList)
        {
            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(@"C:\nurses\ScheduleCSV.csv"))
            {
                file.Write(sche.ReturnSchedule());
            }

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\nurses\ShiftsCSV.csv"))
            {
                file.Write(nurseList.ReturnShifts());
            }
        }
    }
}
