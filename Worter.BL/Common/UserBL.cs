using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Worter.BL.Shared;
using Worter.DAO.Models;
using Worter.Services.Common;

namespace Worter.BL.Common
{
    public class UserBL : BaseBL
    {
        private readonly UserService userService = null;

        public UserBL(WorterContext ctx, int idUser) : base(ctx, idUser)
        {
            userService = new UserService(ctx, idUser);
        }


        public string Test()
        {
            return userService.GetForRead().FirstOrDefault().Username;
        }
    }
}
