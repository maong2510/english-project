namespace Domain.Entities
{
    public class Phonetic
    {
        public long Id { get; set; }
        public long VocabularyId { get; set; }
        public string Text { get; set; }
        public string Audio {  get; set; }
        public string SourceUrl { get; set; }
    }
}
