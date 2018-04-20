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
            sche.SetNight(nurseList);


            sche.Print();

            
            foreach (var item in nurseList.RetTheNurse)
            {
                Console.WriteLine("{0}",item.Name);
                item.Print();
            }

            Console.ReadKey();
        }
    }
}
