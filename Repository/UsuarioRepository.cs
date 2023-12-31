using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_alvaroad29.Models
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string cadenaConexion;

        public UsuarioRepository(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public void Create(Usuario usuario)
        {
            var query = @"INSERT INTO usuario (nombre_de_usuario, contrasenia, rol) VALUES (@name,@contra,@rol);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contra", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery(); // INSERT, DELETE, UPDATE, no retorna nada
                connection.Close();

                if (rowsAffected == 0) // ??
                {
                    throw new Exception();
                }   
            }
        }   
        public void Update(int id, Usuario usuario)
        {
            var query = @"update Usuario set nombre_de_usuario = @nuevoNombre, rol = @rol, contrasenia =  @contra WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nuevoNombre", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contra", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected == 0)
                {
                    throw new Exception("Usuario a actualizar no existe");
                }
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
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (enumRol)Convert.ToInt32(reader["rol"].ToString());
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }
        public Usuario GetById(int id)
        {
            Usuario usuario = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM usuario WHERE id = @idUsuario";
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (enumRol)Convert.ToInt32(reader["rol"].ToString());
                    }
                }
                connection.Close();
            }
            
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            return usuario;
        }
        public void Remove(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"DELETE from Usuario WHERE id = @id;";
                command.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected == 0)
                {
                    throw new Exception("Usuario a eliminar no existe");
                }
            }
        }

        public Usuario Login(string nombre, string contrasenia)
        {
            Usuario usuario = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM usuario WHERE nombre_de_usuario = @nombre AND contrasenia = @contrasenia;";
                command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", contrasenia));
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (enumRol)Convert.ToInt32(reader["rol"].ToString()); // casteo del rol
                    }
                }
                connection.Close();
            }

            return usuario;
        }

        public bool ExistUser(string nombre)
        {
            bool existe = false;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM usuario WHERE nombre_de_usuario = @nombre;";
                command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        existe = true;
                    }
                }
                connection.Close();
            }
            return existe;
        }
    }
}