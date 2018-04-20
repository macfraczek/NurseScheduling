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
        
        public void SetNight(NurseList nurseAllList)
        {
            for (int i = 0; i < schedDayList.Count; i++)  // day 0-34
            {
                for (int j = 0; j < nurseAllList.RetTheNurse.Count; j++) // nurses 
                {
                    //Console.WriteLine("day:{0}\t nurse:{1}",i,j+1);
                    bool test =false;
                    // czy wolna
                    if (nurseAllList.RetTheNurse[j].ListShifts[i] > 4) test = true;
                    // 3 nocne zmiany
                    for (int k = 0,temp =0; k < schedDayList.Count; k++)
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

                    // 2 dni urlopu po nocnych zmianach
                    if (i > 1 && nurseAllList.RetTheNurse[j].ListShifts[i - 1] == 4 && nurseAllList.RetTheNurse[j].ListShifts[i - 2] == 4)
                    {

                        nurseAllList.RetTheNurse[j].ListShifts[i] = 0;
                        if (i < schedDayList.Count - 1)
                            nurseAllList.RetTheNurse[j].ListShifts[i + 1] = 0;
                        test = false;
                        //break;
                    }

                }

            }
        }

        public void Print()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine("\tDAY {0}", i * 7 + j+1);
                    Console.WriteLine("Early 1 : Nurse {0}", schedDayList[j].Early[0]);
                    Console.WriteLine("Early 2 : Nurse {0}", schedDayList[j].Early[1]);
                    Console.WriteLine("Early 3 : Nurse {0}", schedDayList[j].Early[2]);
                    Console.WriteLine("Day 1   : Nurse {0}", schedDayList[j].Day[0]);
                    Console.WriteLine("Day 2   : Nurse {0}", schedDayList[j].Day[1]);
                    Console.WriteLine("Day 3   : Nurse {0}", schedDayList[j].Day[2]);
                    Console.WriteLine("Late 1  : Nurse {0}", schedDayList[j].Late[0]);
                    Console.WriteLine("Late 2  : Nurse {0}", schedDayList[j].Late[1]);
                    Console.WriteLine("Late 3  : Nurse {0}", schedDayList[j].Late[2]);
                    Console.WriteLine("Night   : Nurse {0}",schedDayList[j].Night);
                    Console.WriteLine("----------------------");

                }
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine("\tDAY {0}", i * 7 + j);
                    Console.WriteLine("Early 1 : Nurse {0}", schedDayList[j].Early[0]);
                    Console.WriteLine("Early 2 : Nurse {0}", schedDayList[j].Early[1]);
                    Console.WriteLine("Day 1   : Nurse {0}", schedDayList[j].Day[0]);
                    Console.WriteLine("Day 2   : Nurse {0}", schedDayList[j].Day[1]);
                    Console.WriteLine("Late 1  : Nurse {0}", schedDayList[j].Late[0]);
                    Console.WriteLine("Late 2  : Nurse {0}", schedDayList[j].Late[1]);
                    Console.WriteLine("Night   : Nurse {0}", schedDayList[j].Night);
                    Console.WriteLine("----------------------");
                }
            }
        }
    }
}
