namespace Application.Common.Models.MVocabulary
{
    public class VacabularyResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public StatusReponse Status { get; set; }
        public List<PhoneticResponse> Phonetics { get; set; }
        public List<MeaningResponse> Meanings { get; set; }


    }

    

}
