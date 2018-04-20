using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseScheduling
{
    class Schedule
    {
        List<Days> schedDayList= new List<Days>();

        public Schedule()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    schedDayList.Add(new DaysWeek());
                }
                for (int j = 0; j < 2; j++)
                {
                    schedDayList.Add(new DaysWeekEnd());
                }
            }
        }
        

        public void SetSchedule(NurseList nurseAllList)
        {
            SetNight(nurseAllList);
            //TEMPLATE_LOOP_DAY_NURSES(nurseAllList);
            for (int i = 0; i < 5; i++)
            {
                //early
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {
                    // 3 weekendy na 5
                    if (i > 2)
                        if (nurseAllList.RetTheNurse[j].ListShifts[(i - 1) * 7 + 5] != null && nurseAllList.RetTheNurse[j].ListShifts[(i - 2) * 7 + 5] != null)
                        {
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 0;
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 0;
                        }
                    // pielegniarka wolne
                    if (nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] != null || nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] != null)
                        continue;


                    if (CalculateNurseTimeWeekLeft(nurseAllList.RetTheNurse[j], i) >= 12)
                    {
                        schedDayList[i * 7 + 5].Early[0] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 1;
                        schedDayList[i * 7 + 6].Early[0] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 1;
                        break;
                    }
                }
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {
                    // 3 weekendy na 5
                    if (i > 2)
                        if (nurseAllList.RetTheNurse[j].ListShifts[(i - 1) * 7 + 5] != null && nurseAllList.RetTheNurse[j].ListShifts[(i - 2) * 7 + 5] != null)
                        {
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 0;
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 0;
                        }
                    // pielegniarka wolne
                    if (nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] != null || nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] != null)
                        continue;

                    if (CalculateNurseTimeWeekLeft(nurseAllList.RetTheNurse[j], i) >= 12)
                    {
                        schedDayList[i * 7 + 5].Early[1] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 1;
                        schedDayList[i * 7 + 6].Early[1] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 1;
                        break;
                    }
                }
                
                //day
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {
                    // 3 weekendy na 5
                    if (i > 2)
                        if (nurseAllList.RetTheNurse[j].ListShifts[(i - 1) * 7 + 5] != null && nurseAllList.RetTheNurse[j].ListShifts[(i - 2) * 7 + 5] != null)
                        {
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 0;
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 0;
                        }
                    // pielegniarka wolne
                    if (nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] != null || nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] != null)
                        continue;

                    if (CalculateNurseTimeWeekLeft(nurseAllList.RetTheNurse[j], i) >= 12)
                    {
                        schedDayList[i * 7 + 5].Day[0] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 2;
                        schedDayList[i * 7 + 6].Day[0] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 2;
                        break;
                    }
                }
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {
                    // 3 weekendy na 5
                    if (i > 2)
                        if (nurseAllList.RetTheNurse[j].ListShifts[(i - 1) * 7 + 5] != null && nurseAllList.RetTheNurse[j].ListShifts[(i - 2) * 7 + 5] != null)
                        {
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 0;
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 0;
                        }
                    // pielegniarka wolne
                    if (nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] != null || nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] != null)
                        continue;

                    if (CalculateNurseTimeWeekLeft(nurseAllList.RetTheNurse[j], i) >= 12)
                    {
                        schedDayList[i * 7 + 5].Day[1] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 2;
                        schedDayList[i * 7 + 6].Day[1] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 2;
                        break;
                    }
                }
                // night
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {
                    // 3 weekendy na 5
                    if (i > 2)
                        if (nurseAllList.RetTheNurse[j].ListShifts[(i - 1) * 7 + 5] != null && nurseAllList.RetTheNurse[j].ListShifts[(i - 2) * 7 + 5] != null)
                        {
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 0;
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 0;
                        }
                    // pielegniarka wolne
                    if (nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] != null || nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] != null)
                        continue;

                    if (CalculateNurseTimeWeekLeft(nurseAllList.RetTheNurse[j], i) >= 12)
                    {
                        schedDayList[i * 7 + 5].Late[0] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 3;
                        schedDayList[i * 7 + 6].Late[0] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 3;
                        break;
                    }
                }
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {
                    // 3 weekendy na 5
                    if (i > 2)
                        if (nurseAllList.RetTheNurse[j].ListShifts[(i - 1) * 7 + 5] != null && nurseAllList.RetTheNurse[j].ListShifts[(i - 2) * 7 + 5] != null)
                        {
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 0;
                            nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 0;
                        }
                    // pielegniarka wolne
                    if (nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] != null || nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] != null)
                        continue;

                    if (CalculateNurseTimeWeekLeft(nurseAllList.RetTheNurse[j], i) >= 12)
                    {
                        schedDayList[i * 7 + 5].Late[1] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 5] = 3;
                        schedDayList[i * 7 + 6].Late[1] = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i * 7 + 6] = 3;
                        break;
                    }
                }
                
            }


        }

        private void TEMPLATE_LOOP_DAY_NURSES(NurseList nurseAllList)
        {
            for (int i = 0; i < schedDayList.Count; i++)  // day 0-34
            {
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {

                }
            }
        }

        private static int CalculateNurseTimeWeekLeft(Nurse theNurse,int nrWeek)
        {
            int shiftCount = theNurse.Time;
            for (int i = 0; i < 7; i++)
            {
                if (theNurse.ListShifts[nrWeek * 7 + i] > 0)
                {
                    shiftCount -= 8;
                }
            }
            return shiftCount;
        }

        private void SetNight(NurseList nurseAllList)
        {
            SetNightWeekend(nurseAllList);
            SetNightMonFir(nurseAllList);
            SetNightDayOff(nurseAllList);
        }
        private void SetNightDayOff(NurseList nurseAllList)
        {
            for (int i = 0; i < schedDayList.Count; i++)  // day 0-34
            {
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                    // 2 dni urlopu po nocnych zmianach
                    if (i > 2 && nurseAllList.RetTheNurse[j].ListShifts[i - 1] == 4 && nurseAllList.RetTheNurse[j].ListShifts[i - 2] == 4 && nurseAllList.RetTheNurse[j].ListShifts[i] != 4)
                    {

                        nurseAllList.RetTheNurse[j].ListShifts[i] = 0;
                        if (i < schedDayList.Count - 1)
                            nurseAllList.RetTheNurse[j].ListShifts[i + 1] = 0;
                        //break;
                    }
            }
        }
        private void SetNightMonFir(NurseList nurseAllList)
        {
            for (int i = 0; i < schedDayList.Count; i++)  // day 0-34
            {
                //weekendy osobno
                if (i % 7 == 4 || i % 7 == 5 || i % 7 == 6)
                    continue;
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {
                    //Console.WriteLine("day:{0}\t nurse:{1}",i,j+1);
                    bool test = false;
                    // czy wolna pielegniarka
                    if (nurseAllList.RetTheNurse[j].ListShifts[i] == null) test = true;
                    // 3 nocne zmiany
                    for (int k = 0, temp = 0; k < schedDayList.Count; k++)
                    {
                        if (nurseAllList.RetTheNurse[j].ListShifts[k] == 4) temp++;
                        if (temp >= 3) test = false;
                    }
                    // nowa nocna zmiana
                    if (test)
                    {
                        schedDayList[i].Night = nurseAllList.RetTheNurse[j].Number;
                        nurseAllList.RetTheNurse[j].ListShifts[i] = 4;
                        break;
                    }
                    else
                    {
                        //schedDayList[i].Night = 6;
                        //Console.WriteLine("kod");
                    }


                }

            }
        }
        private void SetNightWeekend(NurseList nurseAllList)
        {
            Random a = new Random();
            var temp = new int?[5];
            bool IsRepeatRandomDay;
            for (int i = 0; i < 5; i++)
            {
                IsRepeatRandomDay = false;
                int randomNurse = a.Next(0, 16);
                temp[i] = randomNurse;
                for (int j = 0; j < i; j++)
                {
                    if (temp[j] == randomNurse)
                    {
                        i--;
                        IsRepeatRandomDay = true;
                        break;
                    }
                }
                if (IsRepeatRandomDay)
                {
                    continue;
                }
                schedDayList[i * 7 + 4].Night = nurseAllList.RetTheNurse[randomNurse].Number;
                nurseAllList.RetTheNurse[randomNurse].ListShifts[i * 7 + 4] = 4;
                schedDayList[i * 7 + 5].Night = nurseAllList.RetTheNurse[randomNurse].Number;
                nurseAllList.RetTheNurse[randomNurse].ListShifts[i * 7 + 5] = 4;
                schedDayList[i * 7 + 6].Night = nurseAllList.RetTheNurse[randomNurse].Number;
                nurseAllList.RetTheNurse[randomNurse].ListShifts[i * 7 + 6] = 4;
            }
        }

        public void Print()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine("\tDAY {0}", i * 7 + j+1);
                    Console.WriteLine("Early 1 : Nurse {0}", schedDayList[i * 7 + j].Early[0]);
                    Console.WriteLine("Early 2 : Nurse {0}", schedDayList[i * 7 + j].Early[1]);
                    Console.WriteLine("Early 3 : Nurse {0}", schedDayList[i * 7 + j].Early[2]);
                    Console.WriteLine("Day 1   : Nurse {0}", schedDayList[i * 7 + j].Day[0]);
                    Console.WriteLine("Day 2   : Nurse {0}", schedDayList[i * 7 + j].Day[1]);
                    Console.WriteLine("Day 3   : Nurse {0}", schedDayList[i * 7 + j].Day[2]);
                    Console.WriteLine("Late 1  : Nurse {0}", schedDayList[i * 7 + j].Late[0]);
                    Console.WriteLine("Late 2  : Nurse {0}", schedDayList[i * 7 + j].Late[1]);
                    Console.WriteLine("Late 3  : Nurse {0}", schedDayList[i * 7 + j].Late[2]);
                    Console.WriteLine("Night   : Nurse {0}",schedDayList[ i * 7 + j].Night);
                    Console.WriteLine("----------------------");

                }
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine("\tDAY {0} Weekend", i * 7 + j+6);
                    Console.WriteLine("Early 1 : Nurse {0}", schedDayList[i * 7 + j+5].Early[0]);
                    Console.WriteLine("Early 2 : Nurse {0}", schedDayList[i * 7 + j+5].Early[1]);
                    Console.WriteLine("Day 1   : Nurse {0}", schedDayList[i * 7 + j+5].Day[0]);
                    Console.WriteLine("Day 2   : Nurse {0}", schedDayList[i * 7 + j+5].Day[1]);
                    Console.WriteLine("Late 1  : Nurse {0}", schedDayList[i * 7 + j+5].Late[0]);
                    Console.WriteLine("Late 2  : Nurse {0}", schedDayList[i * 7 + j+5].Late[1]);
                    Console.WriteLine("Night   : Nurse {0}", schedDayList[i * 7 + j+5].Night);
                    Console.WriteLine("----------------------");
                }
            }
        }
    }
}
