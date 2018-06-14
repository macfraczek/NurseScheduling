namespace NurseScheduling
{
    using System;
    using System.Collections.Generic;

    public static class Tests
    {
        private const int hardPunish = 1000000;
        private const int sPunish1000 = 1000;
        private const int sPunish10 = 10;
        private const int sPunish5 = 5;
        private static int numOfDays = 35;
        private static int numOfWeeks = 5;
        private static int numOfNurses = 16;

        // “week -1” as a starting point for the new 5-week schedule.
        // Shifts are: 1-early, 2-day, 3-late, 4-night, 5-rest.
        // Each row represents a nurse.
        readonly static int[][] week0 = {
           new int[] {4, 4, 5, 5, 5, 5, 5},
           new int[] {3, 3, 4, 4, 5, 5, 5},
           new int[] {5, 5, 1, 1, 4, 4, 4},
           new int[] {1, 1, 5, 5, 5, 1, 1},
           new int[] {1, 1, 1, 5, 5, 1, 1},
           new int[] {3, 5, 5, 5, 1, 3, 3},
           new int[] {3, 3, 5, 5, 3, 3, 3},
           new int[] {5, 5, 5, 1, 1, 2, 2},
           new int[] {5, 5, 1, 1, 1, 2, 2},
           new int[] {1, 1, 3, 3, 3, 5, 5},
           new int[] {2, 3, 3, 3, 5, 5, 5},
           new int[] {5, 2, 3, 3, 3, 5, 5},
           new int[] {2, 2, 2, 2, 2, 5, 5},
           new int[] {2, 2, 2, 5, 5, 5, 5},
           new int[] {5, 5, 5, 2, 2, 5, 5},
           new int[] {5, 5, 2, 2, 2, 5, 5}
        };

        public static long Punishment { get; private set ; }

        public delegate void TestsList(NurseList nurseList, List<Days> schedDayList);
        public static TestsList startTest;

        public static void InitializeTests(NurseList nurseList, List<Days> schedDayList)
        {
            Punishment = 0;
            startTest = TestHardConst_1;
            startTest += TestHardConst_2;
            startTest += TestHardConst_3;
            startTest += TestHardConst_4;
            startTest += TestHardConst_5;
            startTest += TestHardConst_6;
            startTest += TestHardConst_7;
            startTest += TestHardConst_8;
            startTest += TestHardConst_9;
            startTest += TestHardConst_10;
            startTest += TestSoftConst_1;
            startTest += TestSoftConst_3;
            startTest += TestSoftConst_6;
            startTest += TestSoftConst_8;
            startTest += TestSoftConst_11;
            startTest += TestSoftConst_12;

      
            startTest(nurseList, schedDayList);

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
                            Punishment += hardPunish;

                        if (schedDayList[i].Day[k] == 0)
                            Punishment += hardPunish;

                        if (schedDayList[i].Late[k] == 0)
                            Punishment += hardPunish;
                    }

                }
                for (int j = 0; j < 2; j++) // weekend
                {
                    for (int k = 0; k < 2; k++)
                    {
                        if (schedDayList[i].Early[k] == 0)
                            Punishment += hardPunish;

                        if (schedDayList[i].Day[k] == 0)
                            Punishment += hardPunish;

                        if (schedDayList[i].Late[k] == 0)
                            Punishment += hardPunish;
                    }
                }

                if (schedDayList[i].Night == 0)
                    Punishment += hardPunish;
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
                        Punishment += hardPunish;
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
                    Punishment += hardPunish;
                }
            }
        }

        // 5. During any period of 24 consecutive hours, at least 11 hours of rest is required.
        public static void TestHardConst_5(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;

                //sprawdzenie czy po ostatnim dniu week'u-1 następuje odpowiedni odpoczynek
                if (week0[k][6] == 3)
                {
                    if (ListShifts[0] == 1 || ListShifts[0] == 2)
                    {
                        Punishment += hardPunish;
                    }
                }

                if (week0[k][6] == 4)
                {
                    if (ListShifts[0] == 4 && ListShifts[0] != 0 && ListShifts[0] != null)
                    {
                        Punishment += hardPunish;
                    }
                }

                for (int i = 0; i < numOfDays-1; i++)
                {   
                    // sprawdzenie, czy po zmianie LATE nastepuje EARLY lub DAY, wtedy kara                  
                    if (ListShifts[i] == 3)
                    {
                        if (ListShifts[i + 1] == 1 || ListShifts[i + 1] == 2)
                        {
                            Punishment += hardPunish;
                        } 
                    } // sprawdzenie, czy po zmianie NIGHT nastepuje NIGHT lub wolne, inaczej kara
                    else if (ListShifts[i] == 4 && ListShifts[i + 1] != 0 && ListShifts[i + 1] != null)
                    {
                        if (ListShifts[i + 1] != 4)
                        {
                            Punishment += hardPunish;
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

                // zliczenie ile następujących po sobie dni pracuje pielęgniarka w ostatnich dniach poprzedniego harmonogramu               
               for(int j = 0; j < 7; ++j)
                {                   
                    if (week0[k][j] != 5)
                    {
                        ++numOfConsecShifts;
                    
                    } else
                        numOfConsecShifts = 0;
                }
               

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
                        Punishment += hardPunish;
                        numOfConsecShifts = 0;
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
                    Punishment += hardPunish;
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

                // po zmianie NIGHT musi być NIGHT lub wolne
                if(week0[k][6] == 4)
                {
                    if (ListShifts[0] != 4 && ListShifts[0] != 0 && ListShifts[0] != null)
                    {
                        Punishment += hardPunish;
                    }
                }

                for (int i = 0; i < numOfDays-1; i++)
                { // czy zmiana jest NIGHT
                    if (ListShifts[i] == 4)
                    {   // sprawdzenie, czy jest mniej niz 8 h odpoczynku
                        if (ListShifts[i+1] != 4 && ListShifts[i + 1] != 0 && ListShifts[i + 1] != null)
                        {
                            Punishment += hardPunish;
                        }
                    }
                }
               
            }
        }

        // 9. Following a series of at least 2 consecutive night shifts a 42 hours rest is required. 
        public static void TestHardConst_9(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                for (int i = 0; i < numOfDays - 3; i++)
                { // czy zmiana jest NIGHT
                    if (ListShifts[i] == 4)
                    {   // sprawdzenie, czy kolejna zmiana jest NIGHT
                        if (ListShifts[i + 1] == 4)
                        {   // sprawdzenie, czy 2 kolejne dni są wolne
                            if (ListShifts[i + 2] != 0 && ListShifts[i + 2] != null  && ListShifts[i + 3] != 0 && ListShifts[i + 3] != null)
                            {
                                Punishment += hardPunish;
                            }
                        }
                    }
                }

            }
        }

        // 10. The number of consecutive night shifts is at most 3.
        public static void TestHardConst_10(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                for (int i = 0; i < numOfDays - 3; i++)
                { // czy 4 zmiany pod rzad są NIGHT
                    if (ListShifts[i] == 4  && ListShifts[i+1] == 4 && ListShifts[i+2] == 4 && ListShifts[i + 3] == 4)
                    {
                        Punishment += hardPunish;
                    }
                }

            }
        }

        // 1.S For the period of Friday 22:00 to Monday 0:00 a nurse should have either no shifts or at least 2 shifts (‘Complete Weekend’).
        public static void TestSoftConst_1(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;
                for (int i = 0; i < numOfWeeks; i++)
                { // czy w piątek po 22:00 wolne
                    if (ListShifts[i * 7 + 4] != 3 && ListShifts[i * 7 + 4] != 4)
                    {   // czy pielegniarka pracuje w Sobotę i Niedzielę
                        if (ListShifts[i * 7 + 5] > 0 && ListShifts[i * 7 + 6] > 0)
                        {
                            continue; 
                        } else
                        {
                            Punishment += sPunish1000;
                        }

                    }
                    else // piątek po 22:00 pracuje
                    { // potrzebna jeszcze jedna zmiana do 'Complete Weekend'
                        if (ListShifts[i * 7 + 5] > 0 || ListShifts[i * 7 + 6] > 0)
                        {
                            continue;
                        } else
                        {
                            Punishment += sPunish1000;
                        }
                    }
                }

            }
        }

        // 3.S For employees with availability of 30-48 hours per week, the length of a series of night shifts should be within the range 2-3. It could be before another series.
        public static void TestSoftConst_3(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                if (nurseList.ListNurse[k].Time >= 30 && nurseList.ListNurse[k].Time <= 48)
                {
                    var ListShifts = nurseList.ListNurse[k].ListShifts;
                    
                    for (int i = 0; i < numOfDays-2; i++) {
                        int numOfConsecNights = 0;
                        // czy zmiana jest NIGHT
                        if (ListShifts[i] == 4)
                        {
                            ++numOfConsecNights;
                            if (ListShifts[i+1] == 4)
                            {
                                ++numOfConsecNights;
                            }

                            if (ListShifts[i + 2] == 4)
                            {
                                ++numOfConsecNights;
                            }
                        }

                        if (numOfConsecNights > 0 && numOfConsecNights != 2 && numOfConsecNights != 3)
                        {
                            Punishment += sPunish1000;
                        }
                        else if (numOfConsecNights == 2 || numOfConsecNights == 3)
                        {
                            i += numOfConsecNights-1;
                        }

                    }
                 
                }
            }
        }

        // 6.S For employees with availability of 30-48 hours per week, within one week the number of shifts is within the range 4-5.
        public static void TestSoftConst_6(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                if (nurseList.ListNurse[k].Time >= 30 && nurseList.ListNurse[k].Time <= 48)
                {
                    var ListShifts = nurseList.ListNurse[k].ListShifts;
                    int numOfShiftsPerWeek;
                    for (int i = 0; i < numOfWeeks; i++)
                    {
                        numOfShiftsPerWeek = 0;
                        for (int j = 0; j < 7; j++)
                        {
                            if (ListShifts[i * 7 + j] > 0) // zliczanie zmian w tygodniu
                            {
                                ++numOfShiftsPerWeek;
                            }
                        }

                        if(numOfShiftsPerWeek != 4 && numOfShiftsPerWeek != 5)
                        {
                            Punishment += sPunish10;
                        }

                    }
                }
            }
        }

        // 8.S For employees with availability of 30-48 hours per week, the length of a series of shifts should be within the range of 4-6.
        public static void TestSoftConst_8(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                if (nurseList.ListNurse[k].Time >= 30 && nurseList.ListNurse[k].Time <= 48)
                {
                    var ListShifts = nurseList.ListNurse[k].ListShifts;
                    int numOfConsecShifts = 0;

                    if(ListShifts[0] > 0)
                    {
                        ++numOfConsecShifts;
                    }

                    for (int i = 1; i < numOfDays; i++)
                    {
                        if (ListShifts[i] == 0 || ListShifts[i] == null)
                        {
                            if (numOfConsecShifts > 0 && (numOfConsecShifts < 4 || numOfConsecShifts > 6))
                            {
                                Punishment += sPunish10;                                
                            }
                            numOfConsecShifts = 0;
                        }
                        else if (ListShifts[i] > 0)
                        {
                            ++numOfConsecShifts;
                        }

                    }
                }
            }
        }
        
        // 11.S For all employees the length of a series of LATE shifts should be within the range of 2-3. It could be within another series.
        public static void TestSoftConst_11(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {               
                    var ListShifts = nurseList.ListNurse[k].ListShifts;
                    int numOfConsecLates = 0;
                   
                    for (int i = 0; i < numOfDays; i++)
                    {
                        if (ListShifts[i] == 3)
                        {
                            ++numOfConsecLates;
                        } // jeżeli zmiana nie jest LATE
                        else if (numOfConsecLates > 0 && numOfConsecLates != 2 && numOfConsecLates != 3)
                        {
                            Punishment += sPunish10;
                            numOfConsecLates = 0;
                        }
                       
                    }
            }           
        }
   
        // 12.S An early shift after a day shift should be avoided.
        public static void TestSoftConst_12(NurseList nurseList, List<Days> schedDayList)
        {
            for (int k = 0; k < numOfNurses; k++)
            {
                var ListShifts = nurseList.ListNurse[k].ListShifts;

                if(week0[k][6] == 2 && ListShifts[0] == 1)
                {
                    Punishment += sPunish5;
                }
                
                for (int i = 0; i < numOfDays-1; i++)
                {
                    if (ListShifts[i] == 2 && ListShifts[i + 1] == 1)
                    {
                        Punishment += sPunish5;
                    }
                }
            }
        }
    }
}
