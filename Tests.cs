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
        private static int numOfDays = 35;
        private static int numOfWeeks = 5;
        private static int numOfNurses = 16;


        public static int Punishment { get => punishment; set => punishment = value; }

        public delegate void TestsList(NurseList nurseList, List<Days> schedDayList);
        public static TestsList startTest;

        public static void InitializeTests(NurseList nurseList, List<Days> schedDayList)
        {
            startTest = TestHardConst_1;
            startTest += TestHardConst_2;
            startTest += TestHardConst_3;
            startTest += TestHardConst_4;
            startTest += TestHardConst_5;
            startTest += TestHardConst_6;
            startTest += TestHardConst_7;
            startTest += TestHardConst_8;

            startTest(nurseList, schedDayList);

            Console.WriteLine($"Punishment : {Tests.Punishment}");
        }

        //1.	Cover needs to be fulfilled (i.e. no shifts must be left unassigned).
        public static void TestHardConst_1(NurseList nurseList, List<Days> schedDayList)
        {
            for (int i = 0; i < numOfWeeks; i++)
            {
                for (int j = 0; j < 5; j++) // poniedzialek-piatek
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (schedDayList[i].Early[k] == 0)
                            punishment += hardPunish;

                        if (schedDayList[i].Day[k] == 0)
                            punishment += hardPunish;

                        if (schedDayList[i].Late[k] == 0)
                            punishment += hardPunish;
                    }

                }
                for (int j = 0; j < 2; j++) // weekend
                {
                    for (int k = 0; k < 2; k++)
                    {
                        if (schedDayList[i].Early[k] == 0)
                            punishment += hardPunish;

                        if (schedDayList[i].Day[k] == 0)
                            punishment += hardPunish;

                        if (schedDayList[i].Late[k] == 0)
                            punishment += hardPunish;
                    }
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
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                for (int i = 0; i < numOfWeeks; i++)
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

        // 4. A nurse must receive at least 2 weekends off duty per 5 week period. 
        // A weekend off duty lasts 60 hours including Saturday 00:00 to Monday 04:00.
        public static void TestHardConst_4(NurseList nurseList, List<Days> schedDayList)
        {
            int freeWeekendHours;
            int freeWeekends;
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                freeWeekends = 0;
                for (int i = 0; i < numOfWeeks; i++) // sprawdzenie weekendu w każdym tygodniu
                {
                    int friday = i * 7 + 4; // indeks Piatku
                    int saturday = i * 7 + 5; // indeks Soboty
                    int sunday = i * 7 + 6; // indeks Niedzieli

                    // sprawdzenie czy w Piatek jest nocna zmiana, jezeli jest, to nie ma szans na wolny weekend
                    if (ListShifts[friday] == 4)
                    {
                        break;
                    }

                    // sprawdzenie czy Sobota i Niedziela są wolne, jezeli nie są, to nie ma szans na wolny weekend
                    if (ListShifts[saturday] != null || ListShifts[saturday] != 0
                        || ListShifts[sunday] != null || ListShifts[sunday] != 0)
                    {
                        break;
                    }

                    freeWeekendHours = 48; // Wolna Sobota i Niedziela

                    // sprawdzenie brzegów weekendu
                    if (ListShifts[friday] == null || ListShifts[friday] == 0 || ListShifts[friday] == 4) // jest wolny weekend, nie ma potrzeby sprawdzac poniedzialku
                    {
                        freeWeekendHours += 17;

                    }
                    else
                    {
                        if (ListShifts[friday] == 3)
                        {
                            freeWeekendHours += 2;
                        }
                        else if (ListShifts[friday] == 2)
                        {
                            freeWeekendHours += 8;
                        }
                        else if (ListShifts[friday] == 1)
                        {
                            freeWeekendHours += 7;
                        }

                        int monday = i * 7;
                        if (ListShifts[monday] == null || ListShifts[monday] == 0 || ListShifts[monday] == 4)
                        {
                            freeWeekendHours += 17;

                        }
                        else if (ListShifts[monday] == 3)
                        {
                            freeWeekendHours += 14;
                        }
                        else if (ListShifts[monday] == 2)
                        {
                            freeWeekendHours += 7;
                        }
                        else if (ListShifts[monday] == 1)
                        {
                            freeWeekendHours += 8;
                        }

                    }

                    if (freeWeekendHours >= 60)
                    {
                        ++freeWeekends;
                    }
                }

                if (freeWeekends < 2)
                {
                    punishment += hardPunish;
                }
            }
        }

        // 5. During any period of 24 consecutive hours, at least 11 hours of rest is required.
        public static void TestHardConst_5(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                for (int i = 0; i < numOfDays-1; i++)
                {
                    // sprawdzenie, czy po zmianie LATE nastepuje LATE, NIGHT lub wolne, inaczej kara
                    if (ListShifts[i] == 3)
                    {
                        if (ListShifts[i + 1] != 3 && ListShifts[i + 1] != 4 && ListShifts[i + 1] != 0 && ListShifts[i + 1] != null)
                        {
                            punishment += hardPunish;
                        } 
                    } // sprawdzenie, czy po zmianie NIGHT nastepuje NIGHT lub wolne, inaczej kara
                    else if (ListShifts[i] == 4 && ListShifts[i + 1] != 0 && ListShifts[i + 1] != null)
                    {
                        if (ListShifts[i + 1] != 4)
                        {
                            punishment += hardPunish;
                        }                      
                    }
                }
            }
        }
        // 6. The number of consecutive shifts(workdays) is at most 6.
        public static void TestHardConst_6(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                int numOfConsecShifts = 0;
                for (int i = 0; i < numOfDays; i++)
                {   // jeżeli dzień wolny to resetuje liczbę następujących po sobie zmian
                    if (ListShifts[i] == 0 || ListShifts[i] == null)
                    {
                        numOfConsecShifts = 0;
                    } else
                    {
                        ++numOfConsecShifts;
                    }

                    if(numOfConsecShifts > 6)
                    {
                        punishment += hardPunish;
                    }
                }
            }
        }

        // 7. The maximum number of night shifts is 3 per period of 5 consecutive weeks.
        public static void TestHardConst_7(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                int numOfNights = 0;
                for (int i = 0; i < numOfDays; i++)
                { // czy zmiana jest NIGHT
                    if(ListShifts[i] == 4)
                    {
                        ++numOfNights;
                    }
                }
                if(numOfNights > 3)
                {
                    punishment += hardPunish;
                }
            }
        }

        // 8. A night shift has to be followed by at least 14 hours rest. 
        // An exception is that once in a period of 21 days for 24 consecutive hours, 
        // the resting time may be reduced to 8 hours.
        public static void TestHardConst_8(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                for (int i = 0; i < numOfDays-1; i++)
                { // czy zmiana jest NIGHT
                    if (ListShifts[i] == 4)
                    {   // sprawdzenie, czy jest mniej niz 8 h odpoczynku
                        if (ListShifts[i+1] != 4 && ListShifts[i + 1] != 0 && ListShifts[i + 1] != null)
                        {
                            punishment += hardPunish;
                        }
                    }
                }
               
            }
        }
    }
}
