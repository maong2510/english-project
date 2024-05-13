namespace Domain.Entities
{
    public class MeaningDefinition
    {
        public long Id { get; set; }
        public string Definition {  get; set; }
        public string Example { get; set; }
        public string Synonyms { get; set; }
        public string Antonyms { get; set; }
    }
}
