using System;
using System.Collections.Generic;

namespace Worter.DAO.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            InverseIdUsuarioCreacionNavigation = new HashSet<Usuario>();
            InverseIdUsuarioModificacionNavigation = new HashSet<Usuario>();
        }

        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public bool Activo { get; set; }

        public Usuario IdUsuarioCreacionNavigation { get; set; }
        public Usuario IdUsuarioModificacionNavigation { get; set; }
        public ICollection<Usuario> InverseIdUsuarioCreacionNavigation { get; set; }
        public ICollection<Usuario> InverseIdUsuarioModificacionNavigation { get; set; }
    }
}
