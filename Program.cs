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

            NurseList nurseList = new NurseList();
            var sche = new Schedule();
            sche.SetSchedule(nurseList);

            //sche.WriteSchedule();
            sche.WriteScheduleTable();


            nurseList.WriteShiftsTable();
            nurseList.WriteShifts();


            Console.ReadKey();
        }
    }
}
