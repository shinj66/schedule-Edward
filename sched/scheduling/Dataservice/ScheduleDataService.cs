using ScheduleApp.Models;
using System.Collections.Generic;

namespace Dataservice
{
    public class ScheduleDataService
    {
        ISchedule _dataservice;

        public ScheduleDataService(ISchedule schedkDataService)
        {
            _dataservice = schedkDataService;
        }

        public void Add(Schedule sched)
        {
            _dataservice.Add(sched);
        }

        public List<Schedule> GetSchedule()
        {
            return _dataservice.GetSchedule();
        }

        public void Update(int index, Schedule sched)
        {
            _dataservice.Update(index, sched);
        }

        
        public void Delete(int index)
        {
            _dataservice.Delete(index);
        }
    }
}