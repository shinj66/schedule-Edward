using System;
using System.Collections.Generic;
using ScheduleApp.Business;
using ScheduleApp.Models;

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
                        Console.WriteLine($"{i + 1}. {schedules[i].Subject}");
                    }

                    Console.WriteLine($"{schedules.Count + 1}. Back");

                    Console.Write("Choose a subject to view details: ");

                    if (!int.TryParse(Console.ReadLine(), out int sel) ||
                        sel < 1 || sel > schedules.Count + 1)
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
                    Console.WriteLine($"Day: {selected.Day}");
                    Console.WriteLine($"Time: {selected.Time}");
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
                    var schedules = scheduleService.GetSchedules();

                    if (schedules == null || schedules.Count == 0)
                    {
                        Console.WriteLine("No schedules to update.");
                        continue;
                    }

                    Console.WriteLine("\n=== SELECT SCHEDULE TO UPDATE ===");
                    for (int i = 0; i < schedules.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {schedules[i].Subject}");
                    }

                    Console.Write("Choose entry number: ");

                    if (!int.TryParse(Console.ReadLine(), out int sel) ||
                        sel < 1 || sel > schedules.Count)
                    {
                        Console.WriteLine("Invalid selection.");
                        continue;
                    }

                    int index = sel - 1;
                    var selected = schedules[index];

                    var updatedSchedule = new Schedule();

                    Console.WriteLine($"\nUpdating: {selected.Subject}");

                    Console.Write($"New Subject (Current: {selected.Subject}): ");
                    updatedSchedule.Subject = Console.ReadLine();

                    Console.Write($"New Professor (Current: {selected.Professor}): ");
                    updatedSchedule.Professor = Console.ReadLine();

                    Console.Write($"New Room (Current: {selected.Room}): ");
                    updatedSchedule.Room = Console.ReadLine();

                    Console.Write($"New Day (Current: {selected.Day}): ");
                    updatedSchedule.Day = Console.ReadLine();

                    Console.Write($"New Time (Current: {selected.Time}): ");
                    updatedSchedule.Time = Console.ReadLine();

                    scheduleService.UpdateSchedule(index, updatedSchedule);
                    Console.WriteLine("✅ Schedule updated successfully!");
                }

                
                else if (choice == "4")
                {
                    var schedules = scheduleService.GetSchedules();

                    if (schedules == null || schedules.Count == 0)
                    {
                        Console.WriteLine("No schedules to delete.");
                        continue;
                    }

                    Console.WriteLine("\n=== SELECT SCHEDULE TO DELETE ===");
                    for (int i = 0; i < schedules.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {schedules[i].Subject}");
                    }

                    Console.Write("Choose entry number: ");

                    if (!int.TryParse(Console.ReadLine(), out int sel) ||
                        sel < 1 || sel > schedules.Count)
                    {
                        Console.WriteLine("Invalid selection.");
                        continue;
                    }

                    scheduleService.DeleteSchedule(sel - 1);
                    Console.WriteLine("🗑️ Schedule deleted successfully!");
                }

                else if (choice == "5")
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