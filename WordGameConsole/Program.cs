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

namespace WordGameConsole
{
    public class Message
    {
        public string word { get; set; }
        public string[] also { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {

            HttpResponse<string> response = Unirest.get("https://wordsapiv1.p.mashape.com/words/bump/also")
            .header("X-Mashape-Key", "mpHCxg8hs7mshfJveiHzeH13kpxip1TTyWnjsnXJtVI651pQH1")
            .header("Accept", "application/json")
            .asJson<string>();
            JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Body);


            Message newMessage = jsonResponse.ToObject<Message>();
            
            
            
            //Console.WriteLine(jsonResponse["messages"]);
            //Console.WriteLine(response.Body);
            //Console.ReadLine();

            // var client = new RestClient("https://wordsapiv1.p.mashape.com/words/");
            // var request = new RestRequest("example/definitions?accessToke=[]", Method.GET);
            // //4
            // //client.Authenticator = new HttpBasicAuthenticator("X-Mashape-Key", "mgq84hQUXDmshMjka6ulmfpg3z6cp1JEg79jsnVix0eFEdnpKd");
            // //5
            // var response = client.Execute(request);
            // JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(newMessage.word);
            foreach(var item in newMessage.also)
            {
                Console.WriteLine(item);
            }
            
            Console.ReadLine();
        }
    }
}
