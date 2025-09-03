using System.Data;
using System.Data.SqlClient;
using Entity;

namespace Persistence.Users
{
    public class UserPersistence : IUserPersistence
    {
        private readonly string _connectionString;

        public UserPersistence(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int DeleteUser(int id)
        {
            int response = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("deleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        response = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    response = 0;
                    throw new Exception("Error en eliminar usuario " + ex.Message, ex);
                }
                return response;
            }
        }

        public List<UserNotesSummary> GetAllSumary(DateTime? startDate, DateTime? endDate)
        {
            
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("userReport", connection))
                    {
                        List<UserNotesSummary> notes = new List<UserNotesSummary>();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("id");
                            int nameField = reader.GetOrdinal("name");
                            int positionField = reader.GetOrdinal("cargo");
                            int recordsField = reader.GetOrdinal("registros");
                            int statusField = reader.GetOrdinal("status");
                            while (reader.Read())
                            {
                                UserNotesSummary note = new UserNotesSummary();
                                note.id = reader.GetInt32(idField);
                                note.name = reader.GetString(nameField);
                                note.position = reader.GetString(positionField);
                                note.records = reader.GetInt32(recordsField);
                                note.status = reader.GetBoolean(statusField) ==  true ? "activo" : "inactivo";
                                notes.Add(note);
                            }
                        }
                        return notes;
                    }

                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en obtener el resumen de los datos " + ex.Message, ex );
                }
            }
        }

        public List<User> GetAllUser()
        {
            List<User> users = new List<User>();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("GetAllUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("id");
                            int nameField = reader.GetOrdinal("name");
                            int positionIdField = reader.GetOrdinal("positionId");
                            int positionNameFied = reader.GetOrdinal("position");
                            int dateField = reader.GetOrdinal("creationDate");
                            while (reader.Read())
                            {
                                User user = new User();
                                user.id = reader.GetInt32(idField);
                                user.name = reader.GetString(nameField);
                                user.creationDate = reader.GetDateTime(dateField);
                                user.positionId = reader.GetInt32(positionIdField);
                                user.position = new Position
                                {
                                    id = reader.GetInt32(positionIdField),
                                    name = reader.GetString(positionNameFied),

                                };
                                users.Add(user);
                            }
                            

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Error en listado de usuarios " +ex.Message, ex);
                }
            }
            return users;
        }

        public User GetById(int id)
        {
            User? user = null;
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("GetUserById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("id");
                            int nameField = reader.GetOrdinal("name");
                            int positionIdField = reader.GetOrdinal("positionId");
                            int positionNameFied = reader.GetOrdinal("position");
                            while (reader.Read())
                            {
                                user = new User
                                {
                                    id = reader.GetInt32(idField),
                                    name = reader.GetString(nameField),
                                    positionId = reader.GetInt32(positionIdField),
                                    position = new Position
                                    {
                                        name = reader.GetString(positionNameFied),
                                    }
                                };
                            }
                        }
                    }
                    return user;

                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en obtener usuario por id " + ex.Message, ex);
                }
            }
        }

        public void InsertUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("InsertUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@name", user.name);
                        command.Parameters.AddWithValue("@positionId", user.positionId);
                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Error en insertar usuario " + ex.Message, ex);
                }
            }
        }

        public void UpdateUser(User user)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("updateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", user.id);
                        command.Parameters.AddWithValue("@name", user.name);
                        command.Parameters.AddWithValue("@positionId", user.positionId);
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {

                    throw new Exception("Error en actualizar usuarios " + ex.Message, ex);
                }
            }
        }
    }
}
