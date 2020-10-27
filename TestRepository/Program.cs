
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text.Json.Serialization;
using ExoTaskWebApi.Models.Entities;
using Newtonsoft.Json;


namespace TestRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            //Récupération des données (Get)
            //déclarer un client http
            //using(HttpClient client = CreateHttpClient())
            //{
            //    //je fournis la route à ma requete
            //    HttpResponseMessage httpResponseMessage = client.GetAsync("api/task").Result;

            //    //Je test si j'ai un status code 2xx
            //    //httpResponseMessage.EnsureSuccessStatusCode(); // si pas 2xx => exception
            //    if(httpResponseMessage.IsSuccessStatusCode)
            //    {
            //        //Je lis le contenu
            //        string result = httpResponseMessage.Content.ReadAsStringAsync().Result;

            //        //je désérialise le json en instance d'objet
            //        IEnumerable<Task> tasks = JsonConvert.DeserializeObject<Task[]>(result);

            //        foreach (Task task in tasks)
            //        {
            //            Console.WriteLine(task.Title);
            //        }
            //    }
            //    else
            //    {
            //        //J'affiche en cas de code non 2xx
            //        Console.WriteLine($"Status code {httpResponseMessage.StatusCode} : {httpResponseMessage.ReasonPhrase}");
            //    }
                
            //}

            //Récupération des données (Post)
            //using (HttpClient client = CreateHttpClient())
            //{
            //    //j'instancie mon objet
            //    var content = new { Title = "Faire le repository pour qui se connectera à l'api..." };

            //    //je sérialise l'objet
            //    string json = JsonConvert.SerializeObject(content);
            //    //je défini contenu http pour la requete et lui dit que c'est du json
            //    HttpContent httpContent = new StringContent(json);
            //    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //    //je fournis la route à ma requete et son contenu
            //    HttpResponseMessage httpResponseMessage = client.PostAsync("api/task", httpContent).Result;

            //    //Je test si j'ai un status code 2xx
            //    //httpResponseMessage.EnsureSuccessStatusCode(); // si pas 2xx => exception
            //    if (httpResponseMessage.IsSuccessStatusCode)
            //    {
            //        //Je lis le contenu
            //        string result = httpResponseMessage.Content.ReadAsStringAsync().Result;

            //        //je désérialise le json en instance d'objet
            //        Task task = JsonConvert.DeserializeObject<Task>(result);

            //        Console.WriteLine($"{task.Id} : {task.Title} ({task.Created})");
            //    }
            //    else
            //    {
            //        //J'affiche en cas de code non 2xx
            //        Console.WriteLine($"Status code {httpResponseMessage.StatusCode} : {httpResponseMessage.ReasonPhrase}");
            //    }

            //}

            //Récupération des données (Put)
            //using (HttpClient client = CreateHttpClient())
            //{
            //    //j'instancie mon objet
            //    var content = new { Id = 3002, Title = "Faire le repository pour qui se connectera à l'api...", Done = true };

            //    //je sérialise l'objet
            //    string json = JsonConvert.SerializeObject(content);
            //    //je défini contenu http pour la requete et lui dit que c'est du json
            //    HttpContent httpContent = new StringContent(json);
            //    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //    //je fournis la route à ma requete et son contenu
            //    HttpResponseMessage httpResponseMessage = client.PutAsync("api/task/" + content.Id, httpContent).Result;

            //    //Je test si j'ai un status code 2xx
            //    //httpResponseMessage.EnsureSuccessStatusCode(); // si pas 2xx => exception
            //    if (httpResponseMessage.IsSuccessStatusCode)
            //    {
            //        //Je lis le contenu
            //        string jsonResult = httpResponseMessage.Content.ReadAsStringAsync().Result;

            //        //je désérialise le json en instance d'objet
            //        bool result = JsonConvert.DeserializeObject<bool>(jsonResult);

            //        if(result)
            //            Console.WriteLine("Object mis à jour");
            //    }
            //    else
            //    {
            //        //J'affiche en cas de code non 2xx
            //        Console.WriteLine($"Status code {httpResponseMessage.StatusCode} : {httpResponseMessage.ReasonPhrase}");
            //    }

            //}

            //Récupération des données (Delete)
            using (HttpClient client = CreateHttpClient())
            {
                //j'instancie mon objet
                var content = new { Id = 3002 };

                //je fournis la route à ma requete et son contenu
                HttpResponseMessage httpResponseMessage = client.DeleteAsync("api/task/" + content.Id).Result;

                //Je test si j'ai un status code 2xx
                //httpResponseMessage.EnsureSuccessStatusCode(); // si pas 2xx => exception
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    //Je lis le contenu
                    string jsonResult = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    //je désérialise le json en instance d'objet
                    bool result = JsonConvert.DeserializeObject<bool>(jsonResult);

                    if (result)
                        Console.WriteLine("Object déclaré supprimé");
                }
                else
                {
                    //J'affiche en cas de code non 2xx
                    Console.WriteLine($"Status code {httpResponseMessage.StatusCode} : {httpResponseMessage.ReasonPhrase}");
                }                
            }

            Console.ReadLine();
        }

        static HttpClient CreateHttpClient()
        {
            //HttpClientHandler handler = new HttpClientHandler()
            //{
            //    SslProtocols = SslProtocols.Tls12 //force le protocole de sécurité à utiliser lors de la communication entre le client et le serveur
            //};

            //HttpClient client = new HttpClient(handler) 

            HttpClient client = new HttpClient()
            {
                //Défini l'url du site à utiliser
                BaseAddress = new Uri("https://localhost:5001/") 
            };

            //Mon client demande à recevoir de l'api du XML (application/xml) ou du Json (application/json)
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
