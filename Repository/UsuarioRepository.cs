using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_alvaroad29.Models
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";
        public void Create(Usuario usuario)
        {
            var query = $"INSERT INTO usuario (nombre_de_usuario) VALUES (@name);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));
                connection.Open();
                command.ExecuteNonQuery(); // INSERT, DELETE, UPDATE, no retorna nada
                connection.Close();   
            }
        }   
        public void Update(int id, Usuario usuario)
        {
            var query = $"update Usuario set nombre_de_usuario = @nuevoNombre WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nuevoNombre", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader()) // ExecuteReader() devuelve un obj DataReader(), SELECT
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }
        public Usuario GetById(int id)
        {
            var usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM usuario WHERE id = @idUsuario";
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    }
                }
                connection.Close();
            }

            return usuario;
        }
        public void Remove(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = $"DELETE from Usuario WHERE id = @id;";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}