using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gestionbiblioteca.Models;
using System.Data.SqlClient;
using System.Configuration;

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
            throw new NotImplementedException();
        }

        public Usuario getById(Guid codUsuario)
        {
            Usuario usuario = null;

            const string SQL = "";
            using (SqlConnection conexion = new SqlConnection(conexionString))
            {
                SqlCommand command = conexion.CreateCommand();
            }

            return usuario;
        }

        public Usuario update(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}