using Microsoft.Data.SqlClient;
using ScheduleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ScheduleApp.Data
{
    public class SqlScheduleService : IScheduleDataService
    {
        private string connectionString =
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ScheduleApp;Integrated Security=True;TrustServerCertificate=True;";
        private string jsonFile = "schedules.json";

        public SqlScheduleService()
        {
            EnsureTableAndSeed();
        }

        private void EnsureTableAndSeed()
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string createTable = @"
                IF OBJECT_ID('TBSchedule','U') IS NULL
                CREATE TABLE TBSchedule (
                    Subject NVARCHAR(100) PRIMARY KEY,
                    Professor NVARCHAR(100),
                    Room NVARCHAR(50),
                    Times NVARCHAR(MAX)
                )";
            using (var cmd = new SqlCommand(createTable, conn)) cmd.ExecuteNonQuery();

            string check = "SELECT COUNT(*) FROM TBSchedule";
            using (var cmd = new SqlCommand(check, conn))
            {
                int count = (int)cmd.ExecuteScalar();
                if (count == 0) SeedDefaultSchedules();
            }
        }

        private void SeedDefaultSchedules()
        {
            var defaultSchedules = new List<Schedule>
            {
                new Schedule
                {
                    Subject="OOP",
                    Professor="Mr. Ed",
                    Room="204",
                    Times=new List<DaySchedule>
                    {
                        new DaySchedule{ Day="Monday", Time="8:30 AM - 12:30 PM"},
                        new DaySchedule{ Day="Saturday", Time="2:30 PM - 5:30 PM"}
                    }
                },
                new Schedule
                {
                    Subject="PATHFIT",
                    Professor="Mrs. Apostol",
                    Room="205",
                    Times=new List<DaySchedule>
                    {
                        new DaySchedule{ Day="Monday", Time="2:00 PM - 5:00 PM"},
                        new DaySchedule{ Day="Wednesday", Time="8:00 AM - 12:00 PM"}
                    }
                }
            };

            foreach (var s in defaultSchedules) AddSchedule(s);
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

            SaveToJson();
        }

        private List<DaySchedule> ParseTimes(string str)
        {
            var list = new List<DaySchedule>();
            if (string.IsNullOrWhiteSpace(str)) return list;

            foreach (var entry in str.Split(','))
            {
                var parts = entry.Split('|');
                if (parts.Length == 2) list.Add(new DaySchedule { Day = parts[0], Time = parts[1] });
            }
            return list;
        }

        private string FormatTimes(List<DaySchedule> times)
        {
            var list = new List<string>();
            foreach (var t in times) list.Add($"{t.Day}|{t.Time}");
            return string.Join(",", list);
        }

        private void SaveToJson()
        {
            string json = JsonSerializer.Serialize(GetAllSchedules(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFile, json);
        }
    }
}
