using System.Data;
using System.Data.SqlClient;
using petclinic.Models;

namespace petclinic.Contexts
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
                                status = (bool)reader["status"]
                            });
                        }
                    }
                }
            }
            return appointments;
        }
        //################################ 2.GetUserAvailability########################################
        
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
                    command.Parameters.AddWithValue("@username", user.username);
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
        public async Task DeleteAppointmentAsync(Appointment appointment)
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
        // ################# 8.ReserveAppointment ###########################
        public async Task ReserveAppointment(Reservation reservation)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("ReserveAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", reservation.username);
                    command.Parameters.AddWithValue("@when", reservation.when);
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