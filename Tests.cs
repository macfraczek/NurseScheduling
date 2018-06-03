using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseScheduling
{

    public static class Tests
    {
        private static int punishment = 0;
        private const int hardPunish = 1000000;

        public static int Punishment { get => punishment; set => punishment = value; }

        public delegate void TestsList(NurseList nurseList, List<Days> schedDayList);
        public static TestsList startTest;
        
        public static void InitializeTests(NurseList nurseList, List<Days> schedDayList)
        {
            startTest = TestHardConst_1;
            startTest += TestHardConst_2;
            startTest += TestHardConst_3;
            
            startTest(nurseList, schedDayList);
            
            Console.WriteLine($"Punishment : {Tests.Punishment}");
        }

        //1.	Cover needs to be fulfilled (i.e. no shifts must be left unassigned).
        public static void TestHardConst_1(NurseList nurseList, List<Days> schedDayList)
        {
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 2)
                        continue;
                    if (schedDayList[i].Early[j] == 0)
                        punishment += hardPunish;

                }
                for (int j = 0; j < 3; j++)
                {
                    if (j == 2)
                        continue;
                    if (schedDayList[i].Day[j] == 0)
                        punishment += hardPunish;

                }
                for (int j = 0; j < 3; j++)
                {
                    if (j == 2)
                        continue;
                    if (schedDayList[i].Late[j] == 0)
                        punishment += hardPunish;

                }
                if (schedDayList[i].Night == 0)
                    punishment += hardPunish;
            }
        }
        //2.	For each day a nurse may start only one shift.
        public static void TestHardConst_2(NurseList nurseList, List<Days> schedDayList)
        {
            // To jest spełnione z założenia gdzie pielegniarka ma miejsce tylko na jedną zmiane.
            return;
        }
        //3.Within a scheduling period a nurse is allowed
        //to exceed the number of hours for whichthey are available for their department by at most 4 hours.
        public static void TestHardConst_3(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < 16; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                for (int i = 0; i < 5; i++)
                {
                    int shiftCount = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        if (ListShifts[i * 7 + j] > 0)
                        {
                            shiftCount += 8;
                        }
                    }
                    if (shiftCount > nurseList.RetTheNurse[k].Time + 4)
                        punishment += hardPunish;
                }
            }
        }
    }
}
