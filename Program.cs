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
            Int32 AvailablePunishment = 48000000;


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
            Console.WriteLine($"\nHard 1 Broken : {Tests.hard1Broken}");

            SaveTo.SaveToCsv(sche,nurseList);


            Console.ReadKey();
        }
    }
}
