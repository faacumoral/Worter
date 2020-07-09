using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMCW.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Worter.API.Controllers.Shared;
using Worter.BL;
using Worter.DAO.Models;
using Worter.DTO.Language;

namespace Worter.API.Controllers
{
    public class LanguageController : AppController
    {
        private readonly LanguageBL languageBl; 
        public LanguageController(WorterContext context, IConfiguration configuration) : base(context, configuration)
        {
            this.languageBl = new LanguageBL(context, configuration);
        }

        [Route("GetAll")]
        [HttpGet]
        [Authorize]
        public ListResult<LanguageDTO> GetAll()
            => languageBl.GetAll();
        
    }
}