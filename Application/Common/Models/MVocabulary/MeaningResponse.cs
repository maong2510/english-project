using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.MVocabulary
{
    public class MeaningResponse
    {
        public long Id { get; set; }
        public long VocabularyId { get; set; }
        public string PartOfSpeech { get; set; }
        public VacabularyResponse Vocabulary { get; set; }
        public List<MeaningDefinitionResponse> MeaningDefinitions { get; set; }

    }
    public class MeaningDefinitionResponse
    {
        public long Id { get; set; }
        public string Definition { get; set; }
        public string Example { get; set; }
        public string Synonyms { get; set; }
        public string Antonyms { get; set; }
    }
}
