using FMCW.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Worter.Common;

namespace Worter.DTO.Language
{
    public class LanguageDTO : BaseDTO, IDropdownItem
    {
        public int IdLanguage { get; set; }
        public string Name { get; set; }

        public int Value { get => IdLanguage; }
        public string Label { get => Name; }
    }
}
