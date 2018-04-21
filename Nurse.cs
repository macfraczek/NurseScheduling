using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseScheduling
{

    public class Nurse
    {
        private int?[] listShifts = new int?[35];


        public string Name { get; set; }
        public int Number { get; set; }
        public int Time { get; private set; }
        public int?[] ListShifts { get => listShifts; set => listShifts = value; }



        public Nurse(int numb,short time)
        {
            Name = "Nurse: "+numb.ToString();
            Time = time;
            Number = numb;
        }
        public void Print()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (listShifts[i * 7 + j] == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write("{0} ", listShifts[i * 7 + j]);
                    }
                }
                Console.WriteLine("");
            }
        }
    }

    public class NurseList
    {
        const short NURSE_COUNT_36 = 12;
        const short NURSE_COUNT_32 = 1;
        const short NURSE_COUNT_20 = 3;
        List<Nurse> listNurse = new List<Nurse>();

        public NurseList()
        {
            int j = 0;
            for (int i = 0; i < NURSE_COUNT_36; i++)
            {
                listNurse.Add(new Nurse(++j, 36));
            }
            for (int i = 0; i < NURSE_COUNT_32; i++)
            {
                listNurse.Add(new Nurse(++j, 32));
            }
            for (int i = 0; i < NURSE_COUNT_20; i++)
            {
                listNurse.Add(new Nurse(++j, 20));
            }
        }

        public List<Nurse> RetTheNurse => listNurse;

        public List<Nurse> ListNurse { get => listNurse; set => listNurse = value; }
    }
}
