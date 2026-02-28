using System;
using System.Diagnostics;
using System.Formats.Asn1;
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
                Console.WriteLine("6. Exit\n");
                Console.Write("Enter a number from 1-6: ");
                

                if (!int.TryParse(Console.ReadLine(), out num))

                {
                    Console.WriteLine("Invalid input, Please choose a number in the schedule: ");
                    continue;
                }

                else if (num == 1)
                {
                    Console.WriteLine("\nOOP \nMonday: 8:30 AM - 12:30 PM \nSaturday: 2:30 PM - 5:30 PM");
                    Console.WriteLine("Professor: Mr. Ed \nRoom 204\n");
                }
                else if (num == 2)
                {
                    Console.WriteLine("\nPATHFIT \nMonday: 2:00 PM - 5:00 PM \nWednesday: 8:00 AM - 12:00 PM");
                    Console.WriteLine("Professor: Mrs. Apostol \nRoom 205\n");
                }
                else if (num == 3)
                {
                    Console.WriteLine("\nFilipino \nWednesday: 2:00 PM - 5:00 PM \nFriday: 2:00 PM - 5:00 PM");
                    Console.WriteLine("Professor: Mr. Mislan \nRoom 308\n");
                }
                else if (num == 4)
                {
                    Console.WriteLine("\nIntegrative Programming \nSunday: 8:30 AM - 12:30 PM \nSunday: 12:30 PM - 3:30 PM");
                    Console.WriteLine("Professor: Ms. Indaleen\nRoom 207\n");
                }
                else if (num == 5)
                {
                    Console.WriteLine("\nComputer Programming \nFriday: 8:30 AM - 12:30 PM \nSaturday: 8:30 AM - 1:30 PM");
                    Console.WriteLine("Professor: Mr. Rowell \nRoom 210\n");
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
