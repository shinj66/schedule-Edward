using System.Collections.Generic;
using ScheduleApp.Models;

namespace ScheduleApp.Data
{
    public class schedulingservice
    {
        private List<Schedule> schedules;   

        public schedulingservice()
        {
            schedules = new List<Schedule>
            {
                new Schedule
                {
                    Subject = "OOP",
                    Professor = "Sir Ed",
                    Room = "204",
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
        }

        
        public void AddSchedule(Schedule schedule)
        {
            schedules.Add(schedule);
        }

      
        public List<Schedule> GetAllSchedules()
        {
            return schedules;
        }

       
        public void Update(int index, Schedule updatedSchedule)
        {
            if (index >= 0 && index < schedules.Count)
            {
                schedules[index] = updatedSchedule;
            }
        }

       
        public void Delete(int index)
        {
            if (index >= 0 && index < schedules.Count)
            {
                schedules.RemoveAt(index);
            }
        }
    }
}