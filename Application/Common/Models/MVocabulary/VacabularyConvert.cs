using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.MVocabulary
{
    public class VacabularyConvert
    {
        public string word { get; set; }
        public string phonetic { get; set; }
        public List<PhoneticConvert> phonetics { get; set; }
        public List<MeaningConvert> meanings { get; set; }
        public List<string> sourceUrls { get; set; }

    }

    public class PhoneticConvert
    {
        public string text { get; set; }
        public string audio { get; set; }
        public string sourceUrl { get; set; }
    }
    public class MeaningConvert
    {
        public string partOfSpeech { get; set; }
        public List<DefinitionConvert> definitions { get; set; }
        public List<string> synonyms { get; set; }
        public List<string> antonyms { get; set; }

    }
    public class DefinitionConvert
    {
        public string definition { get; set; }
        public List<string> synonyms { get; set; }
        public List<string> antonyms { get; set; }
        public string example { get; set; }
    }
}
