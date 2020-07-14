using FMCW.Common.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worter.BL.Shared;
using Worter.Common;
using Worter.Common.Constants;
using Worter.DAO.Models;
using Worter.DTO.Language;

namespace Worter.BL
{
    public class WordBL : BaseBL
    {
        public WordBL(WorterContext context, IConfiguration configuration) : base(context, configuration) { }

        public IntResult Add(TranslateDTO wordDto, int userId)
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

        /// <summary>
        /// returns 10 learns dto, 5 Word => Translations and 5 Translations => Word
        /// </summary>
        /// <param name="idLanguage"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ListResult<LearnDTO> GetLearns(int idLanguage, int userId)
        {
            var wordsToReturn = CONSTANTS.WORDS_TO_RETURN;
            var result = new List<LearnDTO>();
            // check if its at least 5 words
            var words = context
                .Word
                .Include(w => w.Translation)
                .Where(w => w.IdLanguage == idLanguage && w.IdStudent == userId)
                .AsNoTracking()
                .ToList();

            if (words.Count() < wordsToReturn)
                return ListResult<LearnDTO>.Error($"You must add at least {wordsToReturn} words");

            // TODO order by counter based on wrong or correct before answers
            words.Shuffle();

            // word to translations
            foreach (var word in words.Take(wordsToReturn))
            {
                result.Add(new LearnDTO
                {
                    Word = word.Meaning,
                    Translations = word.Translation
                        .Select( t => t.Translate)
                        .ToList(),
                });
            }

            // translation to word
            words.Shuffle();
            var translations = words.SelectMany(w => w.Translation).ToList();
            foreach (var t in translations.Take(wordsToReturn))
            {
                result.Add(new LearnDTO
                {
                    Word = t.Translate,
                    Translations = new List<string> { t.IdWordNavigation.Meaning  }
                });
            }

            // random order
            result.Shuffle();
            return ListResult<LearnDTO>.Ok(result);
        }

        public BoolResult DeleteTranslate(int idTranslate)
        {
            var translate = context.Translation
                .Include(t => t.IdWordNavigation)
                .ThenInclude(w => w.Translation)
                .FirstOrDefault(t => t.IdTranslation == idTranslate);

            if (translate == null)
            {
                return BoolResult.Error("Translate not found");
            }
            context.Entry(translate).State = EntityState.Deleted;

            if (translate.IdWordNavigation.Translation.Count() == 1)
            {
                context.Entry(translate.IdWordNavigation).State = EntityState.Deleted;
            }
            context.SaveChanges();
            return BoolResult.Ok(true);
        }

        public ListResult<TranslateDTO> Get(WordFilterDTO filters, int iduser)
        {
            // TODO replace acentos, umlauts and cases
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
                        new TranslateDTO
                        {
                            IdTranslate = t.IdTranslation,
                            IdLanguage = w.IdLanguage,
                            IdWord = w.IdWord,
                            OriginalMeaning = w.Meaning,
                            TranslateMeaning = t.Translate
                        }
                ))
                .AsNoTracking()
                .ToList();

            return ListResult<TranslateDTO>.Ok(words);
        }
    }
}
