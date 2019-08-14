using StackExchange.Redis;
using System;

namespace ConsoleAppOraculo
{
    static class Program
    {
        static void Main(string[] args)
        {
            string oraculo = string.Empty;

            var connection = ConnectionMultiplexer.Connect("40.77.24.62");
            var db = connection.GetDatabase();

            try
            {
                ISubscriber sub = connection.GetSubscriber();

                sub.Subscribe("Perguntas", (ch, msg) =>
                {
                    Console.WriteLine(msg.ToString());

                    var chave = msg.ToString().Split(":");
                    oraculo = msg;

                    db.HashSet(chave[0].ToString(), "Equipe: Fundao", "Brasilia");
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problemas de comunicação {ex.Message}");
            }

            Console.Read();

        }
    }
}
    

