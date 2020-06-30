using System;
using System.Collections.Generic;
using System.Text;
using Worter.DAO.Models;

namespace Worter.BL.Shared
{
    public abstract class BaseBL
    {
        protected readonly WorterContext _context;
        public BaseBL(WorterContext context)
        {
            _context = context;
        }
    }
}
