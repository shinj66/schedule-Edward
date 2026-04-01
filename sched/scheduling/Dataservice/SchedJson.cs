using ScheduleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Dataservice
{
   public class SchedJson:ISchedule
    {
        private List<Schedule> sc = new List<Schedule>();
        private string _jsonFileName;
        public SchedJson()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/ScheduleJ.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (sc.Count <= 0)
            {
             sc.Add(new Schedule { 
                 Subject = "OOP",
                 Professor =  "Sir. Ed", 
                 Room = "201",
                 Day = "Saturday",
                 Time = "2pm - 7pm"
             });
                sc.Add(new Schedule
                {
                    Subject = "Database",
                    Professor = "Sir John",
                    Room = "305",
                    Day = "Monday",
                    Time = "9am - 10am"
                });

                sc.Add(new Schedule
                {
                    Subject = "Networking",
                    Professor = "Sir Mark",
                    Room = "101",
                    Day = "Saturday",
                    Time = "7am - 10am"
                });

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
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                this.sc = JsonSerializer.Deserialize<List<Schedule>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }
        public void Add(Schedule sched)
        {
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
