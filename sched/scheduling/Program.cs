using ScheduleApp.Data;
using ScheduleApp.Models;
using ScheduleApp.Business;
namespace ScheduleApp.ConsoleUI
{
    public class Program
    {
        static void Main()
        {
            var scheduleService = new appservice();

            while (true)
            {
                Console.WriteLine("\n=== SCHEDULE MENU ===");
                Console.WriteLine("1. View Schedules");
                Console.WriteLine("2. Add Schedule");
                Console.WriteLine("3. Exit");

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    var schedules = scheduleService.GetSchedules();

                    Console.WriteLine("\nYOUR SCHEDULE IN SCHOOL:");
                    for (int i = 0; i < schedules.Count; i++)
                        Console.WriteLine($"{i + 1}. {schedules[i].Subject}");

                    Console.WriteLine($"{schedules.Count + 1}. Back");

                    Console.Write("Choose a subject: ");
                    if (!int.TryParse(Console.ReadLine(), out int sel) || sel < 1 || sel > schedules.Count + 1)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }

                    if (sel == schedules.Count + 1)
                        continue;

                    var selected = schedules[sel - 1];

                    Console.WriteLine($"\nSubject: {selected.Subject}");
                    Console.WriteLine($"Professor: {selected.Professor}");
                    Console.WriteLine($"Room: {selected.Room}");

                    Console.WriteLine($"{selected.Day}: {selected.Time}");
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n=== ADD NEW SCHEDULE ===");

                    Console.Write("Subject: ");
                    string subject = Console.ReadLine();

                    Console.Write("Professor: ");
                    string professor = Console.ReadLine();

                    Console.Write("Room: ");
                    string room = Console.ReadLine();

                    Console.Write("Day: ");
                    string day = Console.ReadLine();

                    Console.Write("Time: ");
                    string time = Console.ReadLine();

                    var newSchedule = new Schedule
                    {
                        Subject = subject,
                        Professor = professor,
                        Room = room,
                        Day = day,
                        Time = time
                    };

                    scheduleService.AddSchedule(newSchedule);

                    Console.WriteLine("✅ Schedule added successfully!");
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
    }
}