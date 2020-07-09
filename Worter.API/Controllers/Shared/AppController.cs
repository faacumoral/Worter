using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Worter.DAO.Models;

namespace Worter.API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppController : ControllerBase
    {
        protected readonly IConfiguration configuration;

        public AppController(WorterContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
        }

    }
}