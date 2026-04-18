using ScheduleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Dataservice
{
    public class SchedJson : ISchedule
    {
        private List<Schedule> sc = new List<Schedule>();
        private string _jsonFileName;

        public SchedJson()
        {
            _jsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScheduleJ.json");
            EnsureFileExists();
            PopulateJsonFile();
        }

        
        private void EnsureFileExists()
        {
            if (!File.Exists(_jsonFileName))
            {
                using (var stream = File.Create(_jsonFileName))
                {
                  
                }

               
                File.WriteAllText(_jsonFileName, "[]");
            }
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (sc == null || sc.Count == 0)
            {
                sc = new List<Schedule>
                {
                    new Schedule
                    {
                        Subject = "OOP",
                        Professor = "Sir. Ed",
                        Room = "201",
                        Day = "Saturday",
                        Time = "2pm - 7pm"
                    },
                    new Schedule
                    {
                        Subject = "Database",
                        Professor = "Sir John",
                        Room = "305",
                        Day = "Monday",
                        Time = "9am - 10am"
                    },
                    new Schedule
                    {
                        Subject = "Networking",
                        Professor = "Sir Mark",
                        Room = "101",
                        Day = "Saturday",
                        Time = "7am - 10am"
                    }
                };

                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var stream = File.Open(_jsonFileName, FileMode.Create))
            {
                JsonSerializer.Serialize(stream, sc, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            try
            {
                string json = File.ReadAllText(_jsonFileName);

                sc = JsonSerializer.Deserialize<List<Schedule>>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Schedule>();
            }
            catch
            {
                
                sc = new List<Schedule>();
            }
        }

        public void Add(Schedule sched)
        {
            RetrieveDataFromJsonFile(); 
            sc.Add(sched);
            SaveDataToJsonFile();
        }

        public List<Schedule> GetSchedule()
        {
            RetrieveDataFromJsonFile();
            return sc;
        }
    }
}