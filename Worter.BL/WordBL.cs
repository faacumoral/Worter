using FMCW.Common.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Worter.BL.Shared;
using Worter.DAO.Models;
using Worter.DTO.Language;

namespace Worter.BL
{
    public class WordBL : BaseBL
    {
        public WordBL(WorterContext context, IConfiguration configuration) : base(context, configuration) { }

        public IntResult Add(WordDTO wordDto)
        {
            var word = new Word
            { 
                IdLanguage = wordDto.IdLanguage,
                OriginalMeaning = wordDto.OriginalMeaning,
                TranslateMeaning = wordDto.TranslateMeaning
            };
            context.Word.Add(word);
            context.SaveChanges();
            return IntResult.Ok(word.IdWord);
        }
    }
}
