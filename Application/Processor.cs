using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Application
{
    public class Processor
    {
        private readonly HttpClient m_Client;

        public Processor( HttpClient p_Client )
        {
            m_Client = p_Client;
        }

        public async Task ExcuteChallangeCodenation( string urlGetFile, string urlSendFile )
        {
            SHA1 sha = new SHA1CryptoServiceProvider( );

            HttpResponseMessage response = await m_Client.GetAsync( urlGetFile );

            string responseBodyAsText = await response.Content.ReadAsStringAsync( );

            AnswerModel answer = JsonConvert.DeserializeObject<AnswerModel>( responseBodyAsText );

            answer.Decifrado = SimpleSubstitutionCipher.Decrypt( answer.Cifrado, answer.NumeroCasas );

            answer.ResumoCriptografico = GetSha1( answer.Decifrado );

            System.IO.File.WriteAllText( @"./answer.json", responseBodyAsText );

        }

        public string GetSha1( string text )
        {
            if ( text == null )
            {
                return string.Empty;
            }

            byte[] message = System.Text.Encoding.ASCII.GetBytes( text );
            byte[] hashValue = GetSha1( message );

            string hashString = string.Empty;
            foreach ( byte x in hashValue )
            {
                hashString += string.Format( "{0:x2}", x );
            }

            return hashString;
        }

        private byte[] GetSha1( byte[] message )
        {
            SHA1Managed hashString = new SHA1Managed( );
            return hashString.ComputeHash( message );
        }
    }
}
