using Entity;
using System.Data.SqlClient;
using System.Data;


namespace Persistence.Notes
{
    public class NotePersistence : INotePersistence
    {
        private readonly string _connectionString;
        public NotePersistence(string connectionString) {
            _connectionString = connectionString;


        }

        public int DeleteNote(int id)
        {
            int response = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("deleteNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        response = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en eliminar nota" + ex.Message, ex);
                }
            }
            return response;
        }

        public List<Note> GetAllNotes()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getNotes", connection))
                    {
                        List<Note> notes = new List<Note>();
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("id");
                            int titleField = reader.GetOrdinal("title");
                            int descriptionField = reader.GetOrdinal("description");
                            int dateField = reader.GetOrdinal("date");
                            while (reader.Read())
                            {
                                Note note = new Note();
                                note.id = reader.GetInt32(idField);
                                note.title = reader.GetString(titleField);
                                note.description = reader.IsDBNull(descriptionField)? null: reader.GetString(descriptionField);
                                note.date = reader.IsDBNull(dateField) ? DateTime.MinValue : reader.GetDateTime(dateField);
                                notes.Add(note);

                            }


                        }
                        return notes;
                    }

                }
                catch (Exception ex)
                {

                    throw new Exception("Error en Obtener todas las notas " + ex.Message, ex);
                }
            }
        }

        public Note GetById(int id)
        {
            Note? note = null;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getNoteById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("id");
                            int titleField = reader.GetOrdinal("title");
                            int descriptionField = reader.GetOrdinal("description");
                            int dateField = reader.GetOrdinal("date");
                            while (reader.Read())
                            {
                                note = new Note
                                {
                                    id = reader.GetInt32(idField),
                                    title = reader.GetString(titleField),
                                    description = reader.IsDBNull(descriptionField) ? "" : reader.GetString(descriptionField),
                                    date = reader.IsDBNull(dateField) ? DateTime.MinValue : reader.GetDateTime(dateField)
                                };
                            }

                        }
                        return note;

                    }
                }
                catch (Exception ex)
                {

                    connection.Close();
                    throw new Exception("Error en obtener nota por id " + ex.Message, ex);
                }

            }
        }

        public DateTime? GetMaxDate()
        {
            DateTime? maxDate = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("select Max(date) from note", connection))
                    {
                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            maxDate = Convert.ToDateTime(result);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Error en obtener la ultima fecha" + ex.Message, ex );
                }
                return maxDate;
            }
        }

        public List<Note> GetNotesByDate(DateTime? startDate, DateTime? endDate)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {

                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getNoteByDate", connection)) {
                       List<Note> notes = new List<Note>();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);
                        SqlDataReader reader  = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("id");
                            int titleField = reader.GetOrdinal("title");
                            int descriptionField = reader.GetOrdinal("description");
                            int dateField = reader.GetOrdinal("date");
                            while (reader.Read())
                            {
                                Note note = new Note();
                                note.id = reader.GetInt32(idField);
                                note.title = reader.GetString(titleField);
                                note.description = reader.IsDBNull(descriptionField) ? null : reader.GetString(descriptionField);
                                note.date = reader.GetDateTime(dateField);
                                notes.Add(note);

                            }
                        }
                        return notes;

                    }

                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en obtener notas por fechas " + ex.Message, ex);
                }
            }
        }


        public void InsertNote(Note note)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("InsertNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@title", note.title);
                        command.Parameters.AddWithValue("@description", note.description);
                        command.Parameters.AddWithValue("@date", note.date);
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {

                    throw new Exception("Error en insertar nota " + ex.Message, ex);
                }
            }
        }

        public void UpdateNote(Note note)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("updateNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", note.id);
                        command.Parameters.AddWithValue("@title", note.title);
                        command.Parameters.AddWithValue("@description", note.description);
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {

                    throw new Exception("Error en actualizar nota " + ex.Message, ex);
                }
            }
        }
    }
}
