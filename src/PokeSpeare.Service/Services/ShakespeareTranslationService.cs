using Newtonsoft.Json;
using PokeSpeare.Service.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace PokeSpeare.Service.Services
{
    public class ShakespeareTranslationService : IShakespeareTranslationService
    {
        private readonly HttpClient _client;

        public ShakespeareTranslationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> Translate(string text)
        {
            ShakespeareTranslationResult translationResult = null;
            try
            {
                var textWithoutLineFeeds = text
                    .Replace('\n', ' ')
                    .Replace('\f', ' ')
                    .Replace('\r', ' ')
                    .Replace("  ", " ");
                var querystring = $"?text={HttpUtility.UrlEncode(textWithoutLineFeeds)}";
                var response = await _client.GetAsync(querystring);
                translationResult = JsonConvert.DeserializeObject<ShakespeareTranslationResult>(response.Content.ReadAsStringAsync().Result);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
            }

            if (translationResult != null && translationResult.Success?.Total > 0)
                return translationResult.Contents.Translated;

            return text;
        }
    }
}
