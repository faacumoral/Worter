using System;
using System.Collections.Generic;
using System.Text;

namespace Worter.DAO
{
    public interface IEntity
    {
        bool Activo { get; set; }
        DateTime FechaCreacion { get; set; }
        DateTime FechaModificacion { get; set; }
        int IdUsuarioCreacion { get; set; }
        int IdUsuarioModificacion { get; set; }
    }
}
}
