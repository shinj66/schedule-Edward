using System.Collections.Generic;

namespace ScheduleApp.Models
{
    public class Schedule
    {
        public string Subject { get; set; }
        public List<DaySchedule> Times { get; set; }
        public string Professor { get; set; }
        public string Room { get; set; }
    }

    public class DaySchedule
    {
        public string Day { get; set; }
        public string Time { get; set; }
    }

    public interface IScheduleDataService
    {
        List<Schedule> GetAllSchedules();
        Schedule? GetScheduleBySubject(string subject);
        void AddSchedule(Schedule schedule);
    }
}
