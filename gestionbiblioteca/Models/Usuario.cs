using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gestionbiblioteca.Models
{
    public class Usuario
    {
      
        private Guid _codUsuario;
        private string _nombre;
        private string _apellidos;
        private DateTime _fNacimiento;
        private string _username;
        private string _password;
        private string _borrado;

        public Usuario()
        {
            _codUsuario = new Guid("-1");
            _nombre = "";
            _apellidos = "";
            _fNacimiento = new DateTime();
            _username = "";
            _password = "";
        }

        public Guid CodUsuario
        {
            get
            {
                return _codUsuario;
            }

            set
            {
                _codUsuario = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _nombre;
            }

            set
            {
                _nombre = value;
            }
        }

        public string Apellidos
        {
            get
            {
                return _apellidos;
            }

            set
            {
                _apellidos = value;
            }
        }

        public DateTime FNacimiento
        {
            get
            {
                return _fNacimiento;
            }

            set
            {
                _fNacimiento = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        public string Borrado
        {
            get
            {
                return _borrado;
            }

            set
            {
                _borrado = value;
            }
        }
    }
}