using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application
{
    class Program
    {
        static void Main( string[] args )
        {
            Console.WriteLine("Iniciando aplicação");

            Processor processor = new Processor( new HttpClient( ) );

            Task.Run( async ( ) =>
            {
                Console.WriteLine($"Url para pegar aquivo [{args[0]}]");
                Console.WriteLine($"Url para enviar aquivo [{args[1]}]");
                Console.WriteLine($"Path answer [{args[2]}]");
                Console.WriteLine("Iniciando a execução do desafio");

                await processor.ExcuteChallangeCodenation( args[0], args[1] );

                Console.WriteLine("Desafio finalizado");

            } ).GetAwaiter( ).GetResult( );

            Console.WriteLine("Encerrando aplicação");
        }
        public static async Task GetDadosWebServiceAsync( )
        {
            var client = new HttpClient( );

            HttpResponseMessage response = await client.GetAsync( 
                "https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token=a0502e8163dd2338d5adc848bae6433a681e1610" );
            if ( response.IsSuccessStatusCode )
            {
                Console.WriteLine( $"Status Code do Response : {( int )response.StatusCode} {response.ReasonPhrase}" );
                string responseBodyAsText = await response.Content.ReadAsStringAsync( );
                Console.WriteLine( $"Recebidos payload de {responseBodyAsText.Length} caracteres" );
                Console.WriteLine( );
                Console.WriteLine( responseBodyAsText );

                System.IO.File.WriteAllText( @"./answer.json", responseBodyAsText );

            }
        }
    }
}
