using System;
using System.Collections.Generic;

namespace Worter.DAO.Models
{
    public partial class Word
    {
        public int IdWord { get; set; }
        public string OriginalMeaning { get; set; }
        public string TranslateMeaning { get; set; }
        public int IdLanguage { get; set; }
        public int IdStudent { get; set; }

        public virtual Language IdLanguageNavigation { get; set; }
        public virtual Student IdStudentNavigation { get; set; }
    }
}
