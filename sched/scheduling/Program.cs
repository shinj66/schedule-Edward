using System;
using ScheduleApp.Business;
using ScheduleApp.Models;

namespace ScheduleApp.ConsoleUI
{
    public class Program
    {
        static void Main()
        {
            var scheduleService = new appservice();

            Console.WriteLine("       SCHEDULE MANAGEMENT       ");

           
            string emailInput;
            while (true)
            {
                Console.Write("Enter your email for Mailtrap notifications: ");
                emailInput = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(emailInput) &&
                    emailInput.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    scheduleService.UserEmail = emailInput;
                    break;  
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Invalid email! Must end with '@gmail.com'. Please try again.\n");
                Console.ResetColor();
            }

            while (true)
            {
                Console.WriteLine("\n=== SCHEDULE MENU ===");
                Console.WriteLine("1. View Schedules");
                Console.WriteLine("2. Add Schedule");
                Console.WriteLine("3. Update Schedule");
                Console.WriteLine("4. Delete Schedule");
                Console.WriteLine("5. Exit");

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    var schedules = scheduleService.GetSchedules();
                    if (schedules == null || schedules.Count == 0)
                    {
                        Console.WriteLine("No schedules found.");
                        continue;
                    }

                    Console.WriteLine("\nYOUR SCHEDULE IN SCHOOL:");
                    for (int i = 0; i < schedules.Count; i++)
                    {
                        var s = schedules[i];
                        Console.WriteLine($"{i + 1}. {s.Subject} | Prof: {s.Professor} | Room: {s.Room} | Day: {s.Day} | Time: {s.Time}");
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n--- ADD NEW SCHEDULE ---");
                    Console.Write("Enter Subject: ");
                    string subject = Console.ReadLine();

                    Console.Write("Enter Professor: ");
                    string professor = Console.ReadLine();

                    Console.Write("Enter Room: ");
                    string room = Console.ReadLine();

                    Console.Write("Enter Day: ");
                    string day = Console.ReadLine();

                    Console.Write("Enter Time: ");
                    string time = Console.ReadLine();

                    scheduleService.AddSchedule(new Schedule
                    {
                        Subject = subject,
                        Professor = professor,
                        Room = room,
                        Day = day,
                        Time = time
                    });

                    Console.WriteLine("Schedule added successfully!");
                }
                else if (choice == "3")
                {
                    var schedules = scheduleService.GetSchedules();
                    if (schedules == null || schedules.Count == 0)
                    {
                        Console.WriteLine("No schedules available to update.");
                        continue;
                    }

                    Console.Write("\nEnter the number of the schedule to update: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= schedules.Count)
                    {
                        Console.WriteLine("\n--- ENTER UPDATED DETAILS ---");
                        Console.Write("Enter Subject: ");
                        string subject = Console.ReadLine();

                        Console.Write("Enter Professor: ");
                        string professor = Console.ReadLine();

                        Console.Write("Enter Room: ");
                        string room = Console.ReadLine();

                        Console.Write("Enter Day: ");
                        string day = Console.ReadLine();

                        Console.Write("Enter Time: ");
                        string time = Console.ReadLine();

                        scheduleService.UpdateSchedule(index - 1, new Schedule
                        {
                            Subject = subject,
                            Professor = professor,
                            Room = room,
                            Day = day,
                            Time = time
                        });

                        Console.WriteLine("Schedule updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }
                else if (choice == "4")
                {
                    var schedules = scheduleService.GetSchedules();
                    if (schedules == null || schedules.Count == 0)
                    {
                        Console.WriteLine("No schedules available to delete.");
                        continue;
                    }

                    Console.Write("\nEnter the number of the schedule to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= schedules.Count)
                    {
                        scheduleService.DeleteSchedule(index - 1);
                        Console.WriteLine("Schedule deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }
                else if (choice == "5")
                {
                    Console.WriteLine("Exiting program...");
                    break;
                }
            }
        }
    }
}