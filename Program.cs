using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CotacaoMercadoBitcoin
{
    class Program
    {
        static double TimeThread = 5; // Tempo em segundos do Loop da Thread

        static void Main(string[] args)
        {
            Thread webApi = new Thread(WebApiThread); // Cria Thread

            webApi.Start(); // Start na Thread
            
            Console.ReadKey();
        }

        private static void WebApiThread()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(TimeThread);

            var timer = new System.Threading.Timer((e) =>
            {
                WebApiCall();
            }, null, startTimeSpan, periodTimeSpan); // Loop na Requisição
        }

        private static void WebApiCall()
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Default);
            HttpWebRequest.DefaultCachePolicy = policy;

            var req = WebRequest.CreateHttp(CriptoCoin.BTC); // Cria a requisição

            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore); // Seta a requisição sem Cache
            req.CachePolicy = noCachePolicy;
            req.Method = "GET";

            try
            {
                var resposta = req.GetResponse(); // Faz a requisição
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados); // Pega a resposta
                object objResponse = reader.ReadToEnd();
                var result = JObject.Parse(objResponse.ToString()); // Parse JSON
                decimal price = Math.Round(Convert.ToDecimal(result["ticker"]["last"]), 2);

                Console.WriteLine(String.Format("Preço Atual : {0} || Horario : {1}", price, DateTime.Now));

                streamDados.Close();
                resposta.Close();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error : " + e.Message);
            }
        }

    }

    class CriptoCoin
    {
        public static string BTC = "https://www.mercadobitcoin.net/api/BTC/ticker/";
        public static string LTC = "https://www.mercadobitcoin.net/api/LTC/ticker/";
        public static string BCH = "https://www.mercadobitcoin.net/api/BCH/ticker/";
    }
}
