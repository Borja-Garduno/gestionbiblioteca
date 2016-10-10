using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gestionbiblioteca.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace gestionbiblioteca.DAL
{
    public class UsuarioRepositoryImp : UsuarioRepository
    {
        private string conexionString = ConfigurationManager.ConnectionStrings["GESTLIBRERIAConnectionString"].ConnectionString;

        public Guid create(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void delete(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public IList<Usuario> getAll()
        {
            List<Usuario> usuarios = null;

            const string SQL = "getAllUsuarios";
            using (SqlConnection conexion = new SqlConnection(conexionString))
            {

                SqlCommand command = conexion.CreateCommand();
                command.CommandText = SQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conexion;
                conexion.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(parseUsuario(reader));
                        }
                    }
                }
            }

            return usuarios;
        }

        public IList<Usuario> getAllNoBorrados()
        {
            throw new NotImplementedException();
        }

        public IList<Usuario> getAllBorradso()
        {
            throw new NotImplementedException();
        }

        public Usuario getById(Guid codUsuario)
        {
            Usuario usuario = null;
            const string SQL = "getByIdUsuario";
            using (SqlConnection conexion = new SqlConnection(conexionString))
            {

                SqlCommand command = conexion.CreateCommand();
                command.CommandText = SQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conexion;
                conexion.Open();
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            usuario = parseUsuario(reader);
                        }
                    }
                }
            }
            return usuario;
        }

        private Usuario parseUsuario(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.CodUsuario = new Guid(reader["codUsuario"].ToString());
            usuario.Nombre = reader["nombre"].ToString();
            usuario.Apellidos = reader["apellidos"].ToString();
            //usuario.FNacimiento = reader["fNacimiento"];
            usuario.Username = reader["username"].ToString();
            usuario.Password = reader["password"].ToString();
            usuario.Borrado = reader["borrado"].ToString();
            return usuario;
        }

        public Usuario update(Usuario usuario)
        {
            throw new NotImplementedException();
        }

    }
}