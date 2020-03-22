using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System;

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
            string pathFile = @"./ answer.json";

            SHA1 sha = new SHA1CryptoServiceProvider( );

            AnswerModel answer = await GetFile( urlGetFile );

            answer.Decifrado = SimpleSubstitutionCipher.Decrypt( answer.Cifrado, answer.NumeroCasas );

            answer.ResumoCriptografico = GetSha1( answer.Decifrado );

            File.WriteAllText( pathFile, JsonConvert.SerializeObject( answer ) );

            var result = await SendFile( urlSendFile, pathFile );

            Console.WriteLine("Processo finalizado.");
            Console.WriteLine($"Score: {result.Score}");
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

        private async Task<AnswerModel> GetFile( string urlGetFile )
        {
            HttpResponseMessage response = await m_Client.GetAsync( urlGetFile );

            string responseBodyAsText = await response.Content.ReadAsStringAsync( );

            AnswerModel answer = JsonConvert.DeserializeObject<AnswerModel>( responseBodyAsText );

            return answer;
        }

        private async Task<ChallengeResponseModel> SendFile( string url, string filePath )
        {
            MultipartFormDataContent form = new MultipartFormDataContent( );

            var fileContent = new ByteArrayContent( await File.ReadAllBytesAsync( filePath ) );

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse( "multipart/form-data" );
            
            form.Add( fileContent, "answer", Path.GetFileName( filePath ) );

            var response = await m_Client.PostAsync( url, form );

            var responseContent = await response.Content.ReadAsStringAsync( );

            ChallengeResponseModel challengeResponse = JsonConvert.DeserializeObject<ChallengeResponseModel>( responseContent );

            return challengeResponse;
        }
    }
}
