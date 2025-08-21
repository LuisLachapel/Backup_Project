

using Entity;
using System.Data.SqlClient;
using System.Data;

namespace Persistence.Positions
{
    public class PositionPersistence : IPositionPersistence
    {

        private readonly string _connectionString;
        
        public PositionPersistence(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int DeletePosition(int id)
        {
            int response = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("deletePosition", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        response = command.ExecuteNonQuery();

                    }

                }
                catch (Exception ex)
                {
                    response = 0;
                    throw new Exception("Error en eliminar posición " + ex.Message,ex);
                }

            }
            return response;
        }

        public List<Position> GetAllPositions()
        {
          List<Position> positions = new List<Position>();
          using(SqlConnection connection = new SqlConnection(_connectionString))
          {
                try
                { 
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getPositions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField  = reader.GetOrdinal("id");
                            int nameField = reader.GetOrdinal("name");
                            int descriptionField = reader.GetOrdinal("description");
                            while (reader.Read())
                            {
                                Position position = new Position();
                                position.id = reader.GetInt32(idField);
                                position.name = reader.GetString(nameField);
                                position.description = reader.IsDBNull(descriptionField) ? null : reader.GetString(descriptionField);
                                positions.Add(position);
                            }
                        }
                        
                    }
                    return positions;

                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en obtener posiciones " +ex.Message, ex);
                }

          }
        }

        public Position GetById(int id)
        {
            Position? position = null;
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("getPositionById", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("id");
                            int nameField = reader.GetOrdinal("name");
                            int descriptionField = reader.GetOrdinal("description");
                            while (reader.Read())
                            {
                                position = new Position
                                {
                                    id = reader.GetInt32(idField),
                                    name = reader.GetString(nameField),
                                    description = reader.IsDBNull(descriptionField) ? "" : reader.GetString(descriptionField)
                                };
                            }
                        }
                        return position;
                    }
                }
                catch (Exception ex)
                {
                     connection.Close();

                    throw new Exception("Error en obtener posición por id " + ex.Message, ex);
                }
            }
        }

        public void InsertPosition(Position position)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("InsertPosition", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@name", position.name);
                        command.Parameters.AddWithValue("@description", position.description);
                        command.ExecuteNonQuery();
                    } 

                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en insertar el cargo " + ex.Message, ex);
                }
            }
        }

        public void UpdatePosition(Position position)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("updatePosition", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", position.id);
                        command.Parameters.AddWithValue("@name", position.name);
                        command.Parameters.AddWithValue("@description", position.description); 
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en actulizar posición " + ex.Message,ex );
                }
            }
        }
    }
}
