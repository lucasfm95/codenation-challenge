using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class ChallengeResponseModel
    {
        [JsonProperty( "score" )]
        public int Score { get; set; }
    }
}
