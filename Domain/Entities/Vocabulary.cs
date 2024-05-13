namespace Domain.Entities
{
    public class Vocabulary
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long StatusId { get; set; }
        public Status  Status { get; set; }
        public List<Phonetic> Phonetics { get; set; }
        public List<Meaning> Meanings { get; set; }

    }
}
