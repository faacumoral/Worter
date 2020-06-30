using FMCW.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worter.BL.Shared;
using Worter.DAO.Models;

namespace Worter.BL
{
    public class StudentBL : BaseBL
    {
        public StudentBL(WorterContext context) : base(context) { }

        public IntResult LoginStudent(string username, string password)
        {
#warning encript password
            var user = _context.Student.FirstOrDefault(s => s.Username == username && s.Password == password);
            return user != null ?
                IntResult.Ok(user.IdStudent):
                IntResult.Error(new Exception("wrong User or password"));
        }
    }
}
