using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseScheduling
{

    public class Nurse
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public short Time { get; set; }
        public int[] ListShifts { get => listShifts; set => listShifts = value; }

        private int[] listShifts = new int[35];


        public Nurse(int numb,short time)
        {
            Name = "Nurse: "+numb.ToString();
            Time = time;
            Number = numb;
            //Console.WriteLine("{0} was created - {1} h/week.",Name,Time);

            for (int i = 0; i < listShifts.Length; i++)
            {
                listShifts[i] = 8;
            }
        }
        public void Print()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    // osiem to domyslnie brak zmiany, a czemu 8 ? bo tak, 8 zmian na pewno nie bedzie.
                    if (listShifts[i * 7 + j] == 8)
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
