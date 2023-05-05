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
                                when = reader["when"].ToString()
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
                    var res = await command.ExecuteNonQueryAsync();
                    if (res > 0)
                        return user;
                }
            }
            return null;
        }
        // ###################### 4.UpdatePassword  #################################
        public async Task<User> UpdatePasswordAsync(User user)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdatePassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", user.username);
                    command.Parameters.AddWithValue("@password", user.password);
                    var res = await command.ExecuteNonQueryAsync();
                    if (res > 0)
                        return user;
                }
            }
            return null;
        }
        // #####################  5.DeleteUser #########################
        public async Task<string> DeleteUserAsync(String username)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", username);
                    var res = await command.ExecuteNonQueryAsync();
                    if (res > 0)
                        return "Deleted";
                }
            }
            return "Failed";
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
                    var res = await command.ExecuteNonQueryAsync();
                    if (res >0)
                        return appointment;
                }
            }
            return null;
        }

        // ################# 7.DeleteAppointment ##############################
        public async Task<string> DeleteAppointmentAsync(string dateTime)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DeleteAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@when", dateTime);
                    var res = await command.ExecuteNonQueryAsync();
                    if (res > 0)
                        return "Deleted";
                }
            }
            return "Failed";
        }
        // ################# 8.ReserveAppointment ###########################
        public async Task<Reservation> ReserveAppointment(Reservation reservation)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("ReserveAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", reservation.username);
                    command.Parameters.AddWithValue("@appointment", reservation.when);
                    var res= await command.ExecuteNonQueryAsync();
                    if (res > 0)
                        return reservation;
                }
            }
            return null;
        }
        // ################# 9.Login ##########################

        public async Task<User> Login(User user)
        {
            using (var connection = new SqlConnection(_connetionString))
            {
                connection.Open();

                string storedProcedureName = "Login";

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // add input parameters to the command
                    command.Parameters.AddWithValue("@username", user.username);
                    command.Parameters.AddWithValue("@password", user.password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        user = null;
                        while (reader.Read())
                        {
                            return new User
                            {
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