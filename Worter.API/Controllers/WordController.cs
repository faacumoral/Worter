using FMCW.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;
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
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.wordBl = new WordBL(context, configuration);
        }

        #region POST
        [Route("AddTranslate")]
        [HttpPost]
        public IntResult AddTranslate([FromBody]TranslateDTO word)
            => wordBl.Add(word, UserId);

        [Route("DeleteTranslate")]
        [HttpPost]
        public BoolResult DeleteTranslate([FromBody]int IdTranslate)
           => wordBl.DeleteTranslate(IdTranslate);
        #endregion

        #region MyRegion
        [Route("Get")]
        [HttpGet]
        public ListResult<TranslateDTO> Get([FromQuery]WordFilterDTO filters)
            => wordBl.Get(filters, UserId);

        [Route("GetLearns")]
        [HttpGet]
        public ListResult<LearnDTO> GetLearns([FromQuery] int IdLanguage)
            => wordBl.GetLearns(IdLanguage, UserId);
        #endregion
    }
}