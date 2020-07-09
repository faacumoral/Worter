using FMCW.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Worter.API.Controllers.Shared;
using Worter.BL;
using Worter.DAO.Models;
using Worter.DTO.Language;

namespace Worter.API.Controllers
{
    public class WordController : AppController
    {
        private readonly WordBL wordBl;
        public WordController(WorterContext context, IConfiguration configuration) : base(context, configuration)
        {
            this.wordBl = new WordBL(context, configuration);
        }

        [Authorize]
        [Route("Add")]
        [HttpPost]
        public IntResult Add([FromBody]WordDTO word)
            => wordBl.Add(word);
    }
}