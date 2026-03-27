using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ScheduleApp.Models;

namespace ScheduleApp.Data
{
    public class JsonScheduleService : IScheduleDataService
    {
        private readonly string jsonFilePath;
        private List<Schedule> schedules;

        public JsonScheduleService(string filePath)
        {
            jsonFilePath = filePath;

            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                schedules = JsonSerializer.Deserialize<List<Schedule>>(json) ?? new List<Schedule>();
            }
            else
            {
               
                schedules = new List<Schedule>
                {
                    new Schedule
                    {
                        Subject = "OOP",
                        Times = new List<DaySchedule>
                        {
                            new DaySchedule { Day = "Monday", Time = "8:30 AM - 12:30 PM" },
                            new DaySchedule { Day = "Saturday", Time = "2:30 PM - 5:30 PM" }
                        },
                        Professor = "Mr. Ed",
                        Room = "204"
                    },
                    new Schedule
                    {
                        Subject = "PATHFIT",
                        Times = new List<DaySchedule>
                        {
                            new DaySchedule { Day = "Monday", Time = "2:00 PM - 5:00 PM" },
                            new DaySchedule { Day = "Wednesday", Time = "8:00 AM - 12:00 PM" }
                        },
                        Professor = "Mrs. Apostol",
                        Room = "205"
                    },
                    new Schedule
                    {
                        Subject = "FILIPINO",
                        Times = new List<DaySchedule>
                        {
                            new DaySchedule { Day = "Wednesday", Time = "2:00 PM - 5:00 PM" },
                            new DaySchedule { Day = "Friday", Time = "2:00 PM - 5:00 PM" }
                        },
                        Professor = "Mr. Mislan",
                        Room = "206"
                    },
                    new Schedule
                    {
                        Subject = "INTEGRATIVE PROGRAMMING",
                        Times = new List<DaySchedule>
                        {
                            new DaySchedule { Day = "Tuesday", Time = "8:30 AM - 12:30 PM" },
                            new DaySchedule { Day = "Tuesday", Time = "12:30 PM - 3:30 PM" }
                        },
                        Professor = "Ms. Indaleen",
                        Room = "207"
                    },
                    new Schedule
                    {
                        Subject = "COMPUTER PROGRAMMING",
                        Times = new List<DaySchedule>
                        {
                            new DaySchedule { Day = "Friday", Time = "8:30 AM - 12:30 PM" },
                            new DaySchedule { Day = "Saturday", Time = "8:30 AM - 1:30 PM" }
                        },
                        Professor = "Mr. Rowell",
                        Room = "208"
                    }
                };

                SaveChanges(); 
            }
        }

        public List<Schedule> GetAllSchedules() => schedules;

        public Schedule? GetScheduleBySubject(string subject) =>
            schedules.FirstOrDefault(s => s.Subject == subject);

        public void AddSchedule(Schedule schedule)
        {
            schedules.Add(schedule);
            SaveChanges();
        }

        private void SaveChanges()
        {
            string json = JsonSerializer.Serialize(schedules, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, json);
        }
    }
}
