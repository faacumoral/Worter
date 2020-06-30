using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Worter.API.Controllers.Shared;
using Worter.DAO.Models;

namespace Worter.API.Controllers
{
    public class WeatherForecastController : AppController
    {
        private readonly WorterContext _context;

        public WeatherForecastController(WorterContext context)
        {
            _context = context;
        }


    }
}
