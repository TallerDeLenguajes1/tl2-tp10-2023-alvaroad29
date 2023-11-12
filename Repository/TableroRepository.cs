using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_alvaroad29.Models
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";
        public void Create(Tablero tablero){
            var query = @"INSERT INTO Tablero(id_usuario_propietario, nombre, descripcion) VALUES(@idUsuario, @nombre, @descripcion);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario",tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre",tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion",tablero.Descripcion));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();   
            }
        }

        public void Update(int id, Tablero tablero){
            var query = @"UPADTE Tablero SET id_usuario_propietario = @idUsuario, nombre = @nombre, descripcion = @descripcion WHERE id = @idTablero;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Tablero GetById(int id)
        {
            var tablero = new Tablero();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Tablero WHERE id = @idTablero;";
                command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
                connection.Close();
            }

            return tablero;
        }

        public List<Tablero> GetAll()
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }

        public List<Tablero> GetAllById(int id)
        {
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario";
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }

            return tableros;
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM Tablero WHERE id = @id;";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}