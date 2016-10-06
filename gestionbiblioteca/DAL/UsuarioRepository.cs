using gestionbiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionbiblioteca.DAL
{
    interface UsuarioRepository
    {
        IList<Usuario> getAll();
        Usuario getById(Guid codUsuario);
        Usuario update(Usuario usuario);
        void delete(Usuario usuario);
        Guid create(Usuario usuario);
    }
}
