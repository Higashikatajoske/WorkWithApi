using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WorkWithApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Data;

namespace WorkWithApi.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IHttpClientFactory httpClientFactory;
        private static HttpClient httpClient;
        const string requestTokenUrl = "oauth/requesttoken";
        const string accTokenUrlTemp = "oauth/accesstoken";
        const string orderUrlTemp = "orders";

        public HomeController(IHttpClientFactory clientFactory)
        {
            httpClientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            
            Tokens token;
            var request = new HttpRequestMessage(HttpMethod.Get, requestTokenUrl);
            httpClient = httpClientFactory.CreateClient("pixlPark");

            var response = await httpClient.SendAsync(request);
            string jsonAnswer = response.Content.ReadAsStringAsync().Result;
            token = JsonConvert.DeserializeObject<Tokens>(jsonAnswer);

            request = new HttpRequestMessage(HttpMethod.Get, UrlCreator(token, false));
            response = await httpClient.SendAsync(request);
            jsonAnswer = response.Content.ReadAsStringAsync().Result;
            token.RefreshToken = JsonConvert.DeserializeObject<Tokens>(jsonAnswer).RefreshToken;
            token.AccessToken = JsonConvert.DeserializeObject<Tokens>(jsonAnswer).AccessToken;

            request = new HttpRequestMessage(HttpMethod.Get, UrlCreator(token, true));
            response = await httpClient.SendAsync(request);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            JToken jsonResult = json["Result"];
            ViewData["Dictionary"] = new Dictionary<string, JToken>();

            return View(jsonResult);
            
        }

        private string UrlCreator(Tokens tokens, bool isOrders = true)
        {
            string url = "";
            if (isOrders)
            {
                url = orderUrlTemp + "?oauth_token=" + tokens.AccessToken;
            }
            else
            {
                url = accTokenUrlTemp + "?oauth_token=" + tokens.RequestToken + "&grant_type=api&username=" +
                     Tokens.PublicKey + "&password=" + Hash(tokens.RequestToken);
            }
            return url;
        }

        private string Hash(string requestToken)
        {
            string unhashedPassw = requestToken + Tokens.PrivateKey;
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(unhashedPassw));
                StringBuilder sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }


    }
}
