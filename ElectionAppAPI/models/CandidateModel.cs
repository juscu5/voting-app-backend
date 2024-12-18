using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAppAPI.models
{
    public class CandidateModel
    {
        public Guid ID { get; set; }
        public string LASTNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public DateTime BIRTHDATE { get; set; }
        public string CANDIDATE_FOR { get; set; }
        public DateTime CREATED_AT { get; set; }
        public bool STATUS { get; set; }
        public int VOTE_COUNT { get; set; }
        public string COVER_LINKS { get; set; }
        public string USER_LINKS { get; set; }

    }
}
