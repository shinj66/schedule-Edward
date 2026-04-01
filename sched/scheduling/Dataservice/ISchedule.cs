using ScheduleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataservice
{
   public interface ISchedule
    {
        void Add(Schedule sched);
        List<Schedule> GetSchedule();
    }
}
