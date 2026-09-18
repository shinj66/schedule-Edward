using System;
using System.Collections.Generic;
using Dataservice;
using Microsoft.Extensions.Configuration;
using ScheduleApp.Data;
using ScheduleApp.Models;

namespace ScheduleApp.Business
{
    public class appservice
    {
        private readonly ScheduleDataService sc;
        private readonly SchedJson sj;
        private readonly schedulingservice _dataService;
        private readonly EmailService _emailService;

        public string UserEmail { get; set; } = "student@example.com";

        public appservice()
        {
            IConfiguration config = new ConfigurationBuilder()
     .SetBasePath(AppContext.BaseDirectory)
     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
     .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
     .Build();

            sc = new ScheduleDataService(new scheduleDatabase());
            sj = new SchedJson();
            _dataService = new schedulingservice();
            _emailService = new EmailService(config);
        }

        public void AddSchedule(Schedule schedule)
        {
            sc.Add(schedule);
            sj.Add(schedule);
            _dataService.AddSchedule(schedule);

            _emailService.SendNotification(
                UserEmail,
                "Schedule Added Successfully",
                $"You added a new schedule item:\n\nSubject: {schedule.Subject}\nProfessor: {schedule.Professor}\nRoom: {schedule.Room}\nDay: {schedule.Day}\nTime: {schedule.Time}"
            );
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

            _emailService.SendNotification(
                UserEmail,
                "Schedule Updated Successfully",
                $"Schedule item #{index + 1} has been updated to:\n\nSubject: {schedule.Subject}\nProfessor: {schedule.Professor}\nRoom: {schedule.Room}\nDay: {schedule.Day}\nTime: {schedule.Time}"
            );
        }

        public void DeleteSchedule(int index)
        {
            var schedules = GetSchedules();
            string removedSubject = (index >= 0 && index < schedules.Count) ? schedules[index].Subject : "Subject";

            sc.Delete(index);
            sj.Delete(index);
            _dataService.Delete(index);

            _emailService.SendNotification(
                UserEmail,
                "Schedule Deleted Successfully",
                $"The schedule item '{removedSubject}' (item #{index + 1}) was removed from your schedule."
            );
        }
    }
}