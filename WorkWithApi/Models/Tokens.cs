using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WorkWithApi.Models
{
    public class Tokens
    {
        [JsonPropertyName("RequestToken")]
        public string RequestToken { get; set; }
        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }
        [JsonPropertyName("RefreshToken")]
        public string RefreshToken { get; set; }
        public static string PublicKey = "38cd79b5f2b2486d86f562e3c43034f8";
        public static string PrivateKey = "8e49ff607b1f46e1a5e8f6ad5d312a80";

        public Tokens()
        {
        }

        public Tokens (string reqToken)
        {
            RequestToken = reqToken;
        }

        public Tokens(string reqToken, string accessTok)
        {
            RequestToken = reqToken;
            AccessToken = accessTok;
        }
    }
}
