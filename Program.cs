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
            Int32 AvailablePunishment = 40000000;


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
            Console.WriteLine($"\nHard 2 Broken : {Tests.hard2Broken}");
            Console.WriteLine($"\nHard 3 Broken : {Tests.hard3Broken}");
            Console.WriteLine($"\nHard 4 Broken : {Tests.hard4Broken}");
            Console.WriteLine($"\nHard 5 Broken : {Tests.hard5Broken}");
            Console.WriteLine($"\nHard 6 Broken : {Tests.hard6Broken}");
            Console.WriteLine($"\nHard 7 Broken : {Tests.hard7Broken}");
            Console.WriteLine($"\nHard 8 Broken : {Tests.hard8Broken}");
            Console.WriteLine($"\nHard 9 Broken : {Tests.hard9Broken}");
            Console.WriteLine($"\nHard 10 Broken : {Tests.hard10Broken}");
            Console.WriteLine($"\nSoft 1 Broken : {Tests.soft1Broken}");
            Console.WriteLine($"\nSoft 3 Broken : {Tests.soft3Broken}");
            Console.WriteLine($"\nSoft 6 Broken : {Tests.soft6Broken}");
            Console.WriteLine($"\nSoft 8 Broken : {Tests.soft8Broken}");
            Console.WriteLine($"\nSoft 11 Broken : {Tests.soft11Broken}");
            Console.WriteLine($"\nSoft 12 Broken : {Tests.soft12Broken}");

            SaveTo.SaveToCsv(sche,nurseList);


            Console.ReadKey();
        }
    }
}
