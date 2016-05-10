using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using unirest_net.http;
using System.IO;

namespace WordGameConsole
{
    public class Result
    {
        public string definition { get; set; }
        public string[] synonyms { get; set; }
    }

    public class Response
    {
        public string word { get; set; }
        public Result[] results { get; set; }
    }
    class Program
    {
        public static string[] Dictionary = File.ReadAllText("C:\\Users\\epicodus_student\\Desktop\\WordGameConsole\\WordGameConsole\\wordList.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        static void Main(string[] args)
        {

            Random random = new Random();
            int randNumber = random.Next(0, Dictionary.Length);
            var Keyword = Dictionary[randNumber];
            HttpResponse<string> response = Unirest.get("https://wordsapiv1.p.mashape.com/words/" + Keyword + "/")
            .header("X-Mashape-Key", "mpHCxg8hs7mshfJveiHzeH13kpxip1TTyWnjsnXJtVI651pQH1")
            .header("Accept", "application/json")
            .asJson<string>();

            try
            {
                JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Body);
                Response newResponse = jsonResponse.ToObject<Response>();

                if (newResponse.results != null)
                {
                    foreach (var item in newResponse.results)
                    {

                        Console.WriteLine("Definition: " + item.definition);
                        if (item.synonyms != null)
                        {
                            Console.WriteLine("Synonyms: ");
                            foreach (var synonym in item.synonyms)
                            {

                                Console.WriteLine(synonym);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No synonyms for " + Keyword);
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No results for " + Keyword);
                }

                Console.WriteLine("Keyword: " + Keyword);
                Console.ReadLine();

            }
            catch (Exception)
            {
                Console.WriteLine(Keyword + ": returns no value");
                Console.ReadLine();
            }
        }
    }
}
