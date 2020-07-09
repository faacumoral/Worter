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
    public class LanguageBL : BaseBL
    {
        public LanguageBL(WorterContext context, IConfiguration configuration) : base(context, configuration) { }

        public List<LanguageDTO> GetAll()
        {
            return context
                .Language
                .Select(l => new LanguageDTO
                {
                    IdLanguage = l.IdLanguage,
                    Name = l.Name
                }).ToList();
        }

    }
}
