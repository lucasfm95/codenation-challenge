using Newtonsoft.Json;

namespace Application
{
    internal class AnswerModel
    {
        [JsonProperty( "numero_casas" )]
        public int NumeroCasas { get; set; }
        [JsonProperty( "token" )]
        public string Token { get; set; }
        [JsonProperty( "cifrado" )]
        public string Cifrado { get; set; }
        [JsonProperty( "decifrado" )]
        public string Decifrado { get; set; }
        [JsonProperty( "resumo_criptografico" )]
        public string ResumoCriptografico { get; set; }
    }
}
