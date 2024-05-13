namespace Domain.Entities
{
    public class Meaning
    {
        public long Id { get; set; }
        public long VocabularyId { get; set; }
        public string PartOfSpeech { get; set; }
        public Vocabulary Vocabulary { get; set; }
        public List<MeaningDefinition> MeaningDefinitions { get; set; }

    }
}
