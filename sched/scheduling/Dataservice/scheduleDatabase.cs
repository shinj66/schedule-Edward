using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ScheduleApp.Models;

namespace Dataservice
{
    public class scheduleDatabase : ISchedule
    {
        private string connectionString
           = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Scheduling;Integrated Security=True;TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public scheduleDatabase()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetSchedule();

            if (existing.Count == 0)
            {
                Schedule sched1 = new Schedule
                {
                    Subject = "OOP",
                    Professor = "Sir. Ed",
                    Room = "Room 201",
                    Day = "Saturday",
                    Time = "2pm - 7pm"
                };

                Schedule sched2 = new Schedule
                {
                    Subject = "Database",
                    Professor = "Sir John",
                    Room = "305",
                    Day = "Monday",
                    Time = "9am - 10am"
                };

                Schedule sched3 = new Schedule
                {
                    Subject = "Networking",
                    Professor = "Sir Mark",
                    Room = "101",
                    Day = "Saturday",
                    Time = "7am - 10am"
                };

                Add(sched1);
                Add(sched2);
                Add(sched3);
            }
        }

        // ✅ CREATE
        public void Add(Schedule sched)
        {
            var insertStatement =
                "INSERT INTO Schedules VALUES (@Subject,@Professor,@Room,@Day,@Time)";

            SqlCommand insertCommand =
                new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@Subject", sched.Subject);
            insertCommand.Parameters.AddWithValue("@Professor", sched.Professor);
            insertCommand.Parameters.AddWithValue("@Room", sched.Room);
            insertCommand.Parameters.AddWithValue("@Day", sched.Day);
            insertCommand.Parameters.AddWithValue("@Time", sched.Time);

            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<Schedule> GetSchedule()
        {
            var selectStatement =
                "SELECT Subject, Professor, Room, Day, Time FROM Schedules";

            SqlCommand command =
                new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = command.ExecuteReader();

            var sched = new List<Schedule>();

            while (reader.Read())
            {
                Schedule sc = new Schedule();

                sc.Subject = reader["Subject"].ToString();
                sc.Professor = reader["Professor"].ToString();
                sc.Room = reader["Room"].ToString();
                sc.Day = reader["Day"].ToString();
                sc.Time = reader["Time"].ToString();

                sched.Add(sc);
            }

            sqlConnection.Close();

            return sched;
        }

        public void Update(int index, Schedule sched)
        {
            var schedules = GetSchedule();

            if (index < 0 || index >= schedules.Count)
                return;

            string oldSubject = schedules[index].Subject;

            var updateStatement =
                "UPDATE Schedules " +
                "SET Subject=@Subject, Professor=@Professor, Room=@Room, Day=@Day, Time=@Time " +
                "WHERE Subject=@OldSubject";

            SqlCommand command =
                new SqlCommand(updateStatement, sqlConnection);

            command.Parameters.AddWithValue("@Subject", sched.Subject);
            command.Parameters.AddWithValue("@Professor", sched.Professor);
            command.Parameters.AddWithValue("@Room", sched.Room);
            command.Parameters.AddWithValue("@Day", sched.Day);
            command.Parameters.AddWithValue("@Time", sched.Time);
            command.Parameters.AddWithValue("@OldSubject", oldSubject);

            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void Delete(int index)
        {
            var schedules = GetSchedule();

            if (index < 0 || index >= schedules.Count)
                return;

            string subject = schedules[index].Subject;

            var deleteStatement =
                "DELETE FROM Schedules WHERE Subject=@Subject";

            SqlCommand command =
                new SqlCommand(deleteStatement, sqlConnection);

            command.Parameters.AddWithValue("@Subject", subject);

            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}