using FMCW.Common;

namespace Worter.DTO.Language
{
    public class WordDTO : BaseDTO
    {
        public string OriginalMeaning { get; set; }
        public string TranslateMeaning { get; set; }
        public int IdLanguage { get; set; }
    }
}
