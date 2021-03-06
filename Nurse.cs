﻿using System;
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
                Console.Write(" ");
                int shiftCount = 0;
                for (int j = 0; j < 7; j++)
                {
                    if (ListShifts[i * 7 + j] > 0)
                    {
                        shiftCount += 8;
                    }
                }
                Console.WriteLine(shiftCount);
            }
        }
        public string ReturnLine()
        {
            string a="";
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (listShifts[i * 7 + j] == null)
                    {
                        a+=String.Format("-, ");
                    }
                    else
                    {
                        a += String.Format("{0},", listShifts[i * 7 + j]);
                    }
                }
            }
            return a;
        }
    public void PrintLine()
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
                Console.Write("  ");
            }
            Console.WriteLine();
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

        public void WriteShiftsTable()
        {
            Console.Write("No.        ");
            for (int i = 0; i < 35; i++)
            {
                Console.Write("{0,-2}", i + 1);
                if (i%7 == 6)
                    Console.Write("  ");
            }
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------");
            foreach (var item in RetTheNurse)
            {
                Console.Write(String.Format($"{item.Name,-11}"));
                item.PrintLine();
            }
            Console.WriteLine();
        }
        public void WriteShifts()
        {
            foreach (var item in RetTheNurse)
            {
                Console.WriteLine("{0} -{1}h", item.Name, item.Time);
                item.Print();
            }
        }


        public string ReturnShifts()
        {
            string a = "";
            foreach (var item in RetTheNurse)
            {
                a+=item.ReturnLine();
                a += "\r\n";
            }
            return a;
        }
    }
}
