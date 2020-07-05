using FMCW.Common.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worter.BL.Shared;
using Worter.Common;
using Worter.DAO.Models;

namespace Worter.BL
{
    public class StudentBL : BaseBL
    {
        public StudentBL(WorterContext context, IConfiguration configuration) : base(context, configuration) { }

        public IntResult LoginStudent(string username, string password)
        {
            var encrypt_key = configuration.GetEncryptPassword();
            var encodedPassword = FMCW.Seguridad.Encriptador.Encriptar(password, encrypt_key);
            var user = context.Student.FirstOrDefault(s => s.Username == username && s.Password == encodedPassword);
            return user != null ?
                IntResult.Ok(user.IdStudent):
                IntResult.Error(new Exception("Wrong user or password"));
        }
    }
}
