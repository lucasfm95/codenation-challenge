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
                Console.WriteLine("Iniciando a execução do desafio");

                await processor.ExcuteChallangeCodenation( args[0], args[1] );

                Console.WriteLine("Desafio finalizado");

            } ).GetAwaiter( ).GetResult( );

            Console.WriteLine("Encerrando aplicação");
        }
    }
}
