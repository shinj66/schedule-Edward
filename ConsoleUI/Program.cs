using ScheduleApp.Data;
using ScheduleApp.Models;
using System;

namespace ScheduleApp.ConsoleUI
{
    internal class Program
    {
        static void Main()
        {
            IScheduleDataService scheduleService = new SqlScheduleService();

            while (true)
            {
                var schedules = scheduleService.GetAllSchedules();
                Console.WriteLine("\nYOUR SCHEDULE IN SCHOOL:");
                for (int i = 0; i < schedules.Count; i++)
                    Console.WriteLine($"{i + 1}. {schedules[i].Subject}");
                Console.WriteLine($"{schedules.Count + 1}. Exit");

                Console.Write("Choose a number: ");
                if (!int.TryParse(Console.ReadLine(), out int sel) || sel < 1 || sel > schedules.Count + 1)
                {
                    Console.WriteLine("Invalid input.\n");
                    continue;
                }

                if (sel == schedules.Count + 1)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                var selected = schedules[sel - 1];
                Console.WriteLine($"\n{selected.Subject}");
                foreach (var time in selected.Times)
                    Console.WriteLine($"{time.Day}: {time.Time}");
                Console.WriteLine($"Professor: {selected.Professor}");
                Console.WriteLine($"Room: {selected.Room}");
            }
        }
    }
}
