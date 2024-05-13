using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.MVocabulary
{
    public class PhoneticResponse
    {
        public long Id { get; set; }
        public long VocabularyId { get; set; }
        public string Text { get; set; }
        public string Audio { get; set; }
        public string SourceUrl { get; set; }
    }
}
