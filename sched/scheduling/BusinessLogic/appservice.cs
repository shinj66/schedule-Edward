
using System.Collections.Generic;
using Dataservice;
using ScheduleApp.Data;
using ScheduleApp.Models;

namespace ScheduleApp.Business
{
    public class appservice
    {
        ScheduleDataService sc = new ScheduleDataService(new scheduleDatabase());
        SchedJson sj = new SchedJson();
        private readonly schedulingservice _dataService;

        public appservice()
        {
            _dataService = new schedulingservice();
        }

        public void AddSchedule(Schedule schedule)
        {
            sc.Add(schedule);
            sj.Add(schedule);
            _dataService.AddSchedule(schedule);
        }

        public List<Schedule> GetSchedules()
        {
       
            return sc.GetSchedule();

            
        }

     
        public void UpdateSchedule(int index, Schedule schedule)
        {
            sc.Update(index, schedule);
            sj.Update(index, schedule);
            _dataService.Update(index, schedule);
        }

        public void DeleteSchedule(int index)
        {
            sc.Delete(index);
            sj.Delete(index);
            _dataService.Delete(index);
        }
    }
}