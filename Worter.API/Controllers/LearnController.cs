using FMCW.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Worter.API.Controllers.Shared;
using Worter.BL;
using Worter.Common.Constants;
using Worter.DAO.Models;
using Worter.DTO.Language;

namespace Worter.API.Controllers
{
    public class LearnController : AppController
    {
        protected readonly LearnBL learnBl;

        public LearnController(
            WorterContext context, 
            IConfiguration configuration) : base(context, configuration)
        {
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            learnBl = new LearnBL(context, configuration);
        }

        #region POST


        [Route("SaveResultsGetNew")]
        [HttpPost]
        public ListResult<LearnDTO> SaveResultsGetNew([FromBody]List<AnswerDTO> userAnswers)
          => learnBl.SaveResultsGetNew(userAnswers, UserId);

        #endregion

        #region GET
        [Route("GetInitialLearns")]
        [HttpGet]
        public ListResult<LearnDTO> GetInitialLearns([FromQuery] int IdLanguage)
          => learnBl.GetLearns(IdLanguage, UserId, CONSTANTS.WORDS_TO_RETURN * 2 /* first time */);
        #endregion
    }
}