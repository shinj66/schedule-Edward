using Microsoft.Data.SqlClient;
using ScheduleApp.Models;
using System;
using System.Collections.Generic;

namespace ScheduleApp.Data
{
    public class SqlScheduleService : IScheduleDataService
    {
        private string connectionString =
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ScheduleApp;Integrated Security=True;TrustServerCertificate=True;";

        public SqlScheduleService()
        {
            EnsureTableAndSeed();
        }

        // ✅ Create table if missing and seed defaults
        private void EnsureTableAndSeed()
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            // Create table if not exists
            string createTable = @"
                IF OBJECT_ID('TBSchedule', 'U') IS NULL
                CREATE TABLE TBSchedule (
                    Subject NVARCHAR(50),
                    Professor NVARCHAR(50),
                    Room NVARCHAR(50),
                    Times NVARCHAR(MAX)
                )";
            using var cmdCreate = new SqlCommand(createTable, conn);
            cmdCreate.ExecuteNonQuery();

            // Check if empty
            string checkCount = "SELECT COUNT(*) FROM TBSchedule";
            using var cmdCheck = new SqlCommand(checkCount, conn);
            int count = (int)cmdCheck.ExecuteScalar();

            if (count == 0)
                SeedDefaultSchedules();
        }

        private void SeedDefaultSchedules()
        {
            var defaults = new List<Schedule>
            {
                new Schedule
                {
                    Subject = "OOP",
                    Professor = "Mr. Ed",
                    Room = "204",
                    Times = new List<DaySchedule>
                    {
                        new DaySchedule{ Day="Monday", Time="8:30 AM - 12:30 PM" },
                        new DaySchedule{ Day="Saturday", Time="2:30 PM - 5:30 PM" }
                    }
                },
                new Schedule
                {
                    Subject = "PATHFIT",
                    Professor = "Mrs. Apostol",
                    Room = "205",
                    Times = new List<DaySchedule>
                    {
                        new DaySchedule{ Day="Monday", Time="2:00 PM - 5:00 PM" },
                        new DaySchedule{ Day="Wednesday", Time="8:00 AM - 12:00 PM" }
                    }
                },
                new Schedule
                {
                    Subject = "FILIPINO",
                    Professor = "Mr. Mislan",
                    Room = "206",
                    Times = new List<DaySchedule>
                    {
                        new DaySchedule{ Day="Wednesday", Time="2:00 PM - 5:00 PM" },
                        new DaySchedule{ Day="Friday", Time="2:00 PM - 5:00 PM" }
                    }
                },
                new Schedule
                {
                    Subject = "INTEGRATIVE PROGRAMMING",
                    Professor = "Ms. Indaleen",
                    Room = "207",
                    Times = new List<DaySchedule>
                    {
                        new DaySchedule{ Day="Tuesday", Time="8:30 AM - 12:30 PM" },
                        new DaySchedule{ Day="Tuesday", Time="12:30 PM - 3:30 PM" }
                    }
                },
                new Schedule
                {
                    Subject = "COMPUTER PROGRAMMING",
                    Professor = "Mr. Rowell",
                    Room = "208",
                    Times = new List<DaySchedule>
                    {
                        new DaySchedule{ Day="Friday", Time="8:30 AM - 12:30 PM" },
                        new DaySchedule{ Day="Saturday", Time="8:30 AM - 1:30 PM" }
                    }
                }
            };

            foreach (var s in defaults)
                AddSchedule(s);
        }

        public List<Schedule> GetAllSchedules()
        {
            var schedules = new List<Schedule>();
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "SELECT * FROM TBSchedule";
            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                schedules.Add(new Schedule
                {
                    Subject = reader["Subject"].ToString(),
                    Professor = reader["Professor"].ToString(),
                    Room = reader["Room"].ToString(),
                    Times = ParseTimes(reader["Times"].ToString())
                });
            }

            return schedules;
        }

        public Schedule? GetScheduleBySubject(string subject)
        {
            return GetAllSchedules().Find(s => s.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase));
        }

        public void AddSchedule(Schedule schedule)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string query = @"INSERT INTO TBSchedule (Subject, Professor, Room, Times)
                             VALUES (@Subject, @Professor, @Room, @Times)";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Subject", schedule.Subject);
            cmd.Parameters.AddWithValue("@Professor", schedule.Professor);
            cmd.Parameters.AddWithValue("@Room", schedule.Room);
            cmd.Parameters.AddWithValue("@Times", FormatTimes(schedule.Times));
            cmd.ExecuteNonQuery();
        }

        private List<DaySchedule> ParseTimes(string str)
        {
            var list = new List<DaySchedule>();
            if (string.IsNullOrWhiteSpace(str)) return list;

            foreach (var entry in str.Split(','))
            {
                var parts = entry.Split('|');
                if (parts.Length == 2)
                    list.Add(new DaySchedule { Day = parts[0], Time = parts[1] });
            }

            return list;
        }

        private string FormatTimes(List<DaySchedule> times)
        {
            var list = new List<string>();
            foreach (var t in times)
                list.Add($"{t.Day}|{t.Time}");
            return string.Join(",", list);
        }
    }
}
