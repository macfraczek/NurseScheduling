using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NurseScheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 AvailablePunishment = 28000000;


            NurseList nurseList = new NurseList();
            var sche = new Schedule();
            do
            {
                nurseList = new NurseList();
                sche = new Schedule();
                sche.SetSchedule(nurseList);
                Tests.InitializeTests(nurseList, sche.SchedDayList);
            } while (Tests.Punishment > AvailablePunishment);


            //sche.WriteSchedule();
            sche.WriteScheduleTable();

            nurseList.WriteShiftsTable();
            nurseList.WriteShifts();

            Console.WriteLine($"\nPunishment : {Tests.Punishment}");
            Console.WriteLine($"\nHard Broken : {Tests.hardBroken}");

            SaveTo.SaveToCsv(sche,nurseList);


            Console.ReadKey();
        }
    }
}
