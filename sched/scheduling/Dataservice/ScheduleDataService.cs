using ScheduleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
