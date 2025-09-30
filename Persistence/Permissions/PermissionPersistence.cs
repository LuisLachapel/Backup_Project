
using Entity;
using System.Data.SqlClient;
using System.Data;


namespace Persistence.Permissions
{
    public class PermissionPersistence : IPermissionPersitence
    {
        private readonly string _connectionString;
        public PermissionPersistence(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Permission> GetPermissions()
        {
           List<Permission> permissions = new List<Permission>();
           using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("getPermissions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("id");
                            int nameField = reader.GetOrdinal("name");
                            int codeField = reader.GetOrdinal("code");
                            int descriptionField = reader.GetOrdinal("description");
                            while (reader.Read())
                            {
                                Permission permission = new Permission();
                                permission.id = reader.GetInt32(idField);
                                permission.name = reader.GetString(nameField);
                                permission.code = reader.GetString(codeField);
                                permission.description = reader.IsDBNull(descriptionField) ? string.Empty: reader.GetString(descriptionField);
                                permissions.Add(permission);
                            }
                        }
                        return permissions;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en obtener posiciones " + ex.Message, ex);
                }
            }

        }

        public void InsertPermission(Permission permission)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("InsertPermission", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@name", permission.name);
                        command.Parameters.AddWithValue("@code", permission.code);
                        command.Parameters.AddWithValue("@description", permission.description);
                        command.ExecuteNonQuery();
                       
                    }
                }
                catch (SqlException ex)
                {
                    if(ex.Number == 50000)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                    connection.Close();
                    throw new Exception("Error en insertar el permiso " + ex.Message, ex);
                }
            }
        }

        public void UpdatePermission(Permission permission)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("updatePermission", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", permission.id);
                        command.Parameters.AddWithValue("@name", permission.name);
                        command.Parameters.AddWithValue("@code", permission.code);
                        command.Parameters.AddWithValue("@description", permission.description);
                        command.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    if(ex.Number == 50000)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                    connection.Close();
                    throw new Exception("error en editar permisos " + ex.Message, ex);
                }
            }
        }
    }
}
