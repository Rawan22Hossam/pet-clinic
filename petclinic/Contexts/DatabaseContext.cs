using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Models;
using petclinic.Models;

namespace Contexts
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IConfiguration _configuration;
        public string _connetionString;
        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connetionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // ########################### 1.GetAvailableAppointments###################################
        public async Task<List<Appointment>> GetAvailableAppointments()
        {
            var appointments = new List<Appointment>();
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAvailableAppointments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            appointments.Add(new Appointment
                            {
                                ID = (int)reader["ID"],
                                when = (DateTime)reader["when"],
                                status = (string)reader["status"]
                            });
                        }
                    }
                }
            }
            return appointments;
        }
        //################################ 2.GetUserAvailability########################################
        public async Task<bool> GetUserAvailability()
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetUserAvailability", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            User user = new User
                            {
                                ID = (int)reader["ID"],
                                username = (string)reader["username"],
                                password = (string)reader["password"]
                            };
                            return user == null;
                        }
                    }
                }
            }
            return true;
        }
        //#########################  3.Register   ###############################
        public async Task<User> Register(User user)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("Register", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", user.username);
                    command.Parameters.AddWithValue("@password", user.password);
                    await command.ExecuteNonQueryAsync();
                    return user;
                }
            }
        }
        // ###################### 4.UpdatePassword  #################################
        public async Task UpdatePasswordAsync(User user)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdatePassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", user.username);
                    command.Parameters.AddWithValue("@password", user.password);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        // #####################  5.DeleteUser #########################
        public async Task DeleteUserAsync(User user)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", username);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // ################# 6.AddAppointment ##################################
        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@when", appointment.when);
                    await command.ExecuteNonQueryAsync();
                    return appointment;
                }
            }
        }

        // ################# 7.DeleteAppointments ##############################
        public async Task DeleteAppointmentsAsync(Appointment appointment)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DeleteAppointments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@when", appointment.when);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        // ################# 8.ReserveAppointments ###########################
        public async Task ReserveAppointments(Reservation reservation)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("ReserveAppointments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", reservation.username);
                    command.Parameters.AddWithValue("@appointment", reservation.appointment);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        // ################# 9.Login ##########################

        public async Task<User> Login(User user)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("Login", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            return new User
                            {
                                ID = (int)reader["ID"],
                                username = (string)reader["username"],
                                password = (string)reader["password"]
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}