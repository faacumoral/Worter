using System;
using System.Collections.Generic;
using System.Text;
using Worter.DAO.Models;
using Worter.Services.Shared;

namespace Worter.Services.Common
{
    public class UserService : Service<Usuario>
    {
        public UserService(WorterContext ctx, int idUsuario) : base(ctx, idUsuario) { }
    }
}
