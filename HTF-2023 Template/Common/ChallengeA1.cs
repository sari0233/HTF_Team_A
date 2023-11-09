using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ChallengeA1
    {

        HackTheFutureClient _client;

        public ChallengeA1()
        {
            _client = new HackTheFutureClient();
        }

        public async Task<HttpResponseMessage> StartChallengeA1()
        {
            await _client.Login("Team A", "SZRz9Zqpiv");
            var response = await _client.GetAsync("/api/path/a/easy/start");

            return response;
        }

        public async Task<string> GetHieroglyph()
        {
            await _client.Login("Team A", "SZRz9Zqpiv");
            var response = await _client.GetAsync("/api/path/a/easy/puzzle");
            var hieroglyph = await response.Content.ReadAsStringAsync();
            return hieroglyph;

        }

        public string Decipher(string hieroglyph)
        {


            char[] chars = hieroglyph.ToCharArray();

            StringBuilder sb = new StringBuilder();

            foreach (char c in chars)
            {
                if (HieroglyphAlphabet.Characters.ContainsKey(c))
                {
                    sb.Append(HieroglyphAlphabet.Characters[c]);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public async Task<HttpResponseMessage> PostHieroglyph(string decipheredText)
        {
            await _client.Login("Team A", "SZRz9Zqpiv");

            var response = await _client.PostAsJsonAsync("/api/path/a/easy/puzzle", decipheredText);

            return response;

        }

    }
}
