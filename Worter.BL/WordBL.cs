using FMCW.Common.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worter.BL.Shared;
using Worter.DAO.Models;
using Worter.DTO.Language;

namespace Worter.BL
{
    public class WordBL : BaseBL
    {
        public WordBL(WorterContext context, IConfiguration configuration) : base(context, configuration) { }

        public IntResult Add(WordDTO wordDto, int userId)
        {
            // check if word exists
            var word = context.Word.Where(w => w.Meaning == wordDto.OriginalMeaning && w.IdLanguage == wordDto.IdLanguage)
                .Include( w => w.Translation)
                .FirstOrDefault();

            if (word == null)
            {
                word = new Word
                {
                    IdLanguage = wordDto.IdLanguage,
                    Meaning = wordDto.OriginalMeaning,
                    IdStudent = userId
                };
                context.Word.Add(word);
            }
            // check if translation already exists
            else if (word.Translation.Any(t => t.Translate == wordDto.TranslateMeaning))
            {
                return new IntResult 
                { 
                    ResultOperation = ResultOperation.RegisterAlreadyAdd,
                    Success = true
                };
            }
            
            var translation = new Translation
            {
                IdWordNavigation = word,
                Translate = wordDto.TranslateMeaning
            };
            context.Translation.Add(translation);

            context.SaveChanges();
            return IntResult.Ok(word.IdWord);
        }

        public ListResult<WordDTO> Get(WordFilterDTO filters, int iduser)
        {
            var words = context
                .Word
                .Where(
                    w => w.IdStudent == iduser &&
                    w.IdLanguage == filters.IdLanguage &&
                    (w.Translation.Any(t => t.Translate.Contains(filters.Filter)) || w.Meaning.Contains(filters.Filter)))
                .SelectMany(
                w => w
                    .Translation
                    .Select(t =>
                        new WordDTO
                        {
                            IdTranslate = t.IdTranslation,
                            IdLanguage = w.IdLanguage,
                            IdWord = w.IdWord,
                            OriginalMeaning = w.Meaning,
                            TranslateMeaning = t.Translate
                        }
                )).ToList();

            return ListResult<WordDTO>.Ok(words);
        }
    }
}
