using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace schedule
{
    internal class sched
    {
        static void Main(string[] args)
        {
            
            int num;
            while (true)
            {
                Console.WriteLine("YOUR SCHEDULE IN SCHOOL! ");
                Console.Write("Choose a subject: \n");
                Console.WriteLine("1. OOP. ");
                Console.WriteLine("2. PATHFIT. ");
                Console.WriteLine("3. Filipino. ");
                Console.WriteLine("4. Integrative Programming. ");
                Console.WriteLine("5. Computer Programming. ");
                Console.WriteLine("6. Exit ");
                num = Convert.ToInt16(Console.ReadLine());

                if (num == 1)
                {
                    Console.WriteLine("OOP \nMonday: 8:30 AM - 12:30 PM \nSaturday: 2:30 PM - 5:30 PM");
                }
                else if (num == 2)
                {
                    Console.WriteLine("PATHFIT \nMonday: 2:00 PM - 5:00 PM \nWednesday: 8:00 AM - 12:00 PM");
                }
                else if (num == 3)
                {
                    Console.WriteLine("Filipino \nWednesday: 2:00 PM - 5:00 PM \nFriday: 2:00 PM - 5:00 PM");
                }
                else if (num == 4)
                {
                    Console.WriteLine("Integrative Programming \nSunday: 8:30 AM - 12:30 PM \nSunday: 12:30 PM - 3:30 PM");
                }
                else if (num == 5)
                {
                    Console.WriteLine("Computer Programming \nFriday: 8:30 AM - 12:30 PM \nSaturday: 8:30 AM - 1:30 PM");
                }
                else if (num == 6)
                {
                    Console.WriteLine("Exiting the program...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, Please choose a number in the schedule: ");
                }
            }
        }
    }
}
