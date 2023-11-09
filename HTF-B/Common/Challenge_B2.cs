using System;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Common
{
    public class Challenge_B2
    {
        private HackTheFutureClient client;

        public Challenge_B2(HackTheFutureClient client)
        {
            this.client = client;
        }

        public async Task StartChallenge()
        {
            await client.Login("Team A", "SZRz9Zqpiv");
            await client.GetAsync("/api/path/b/medium/start");
        }

        public async Task TestSample()
        {
            await client.Login("Team A", "SZRz9Zqpiv");
            var response = await client.GetAsync("/api/path/b/medium/puzzle");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var strings = JsonConvert.DeserializeObject<string[]>(content);

                var commonHieroglyphs = GetCommonHieroglyphs(strings);

                var postResponse = await client.PostAsJsonAsync($"/api/path/b/medium/puzzle", commonHieroglyphs);

                if (!postResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Failed!");
                }

                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Failed to GET!");
            }
        }

        public string GetCommonHieroglyphs(string[] strings)
        {
            int length = strings[0].Length;

            string commonHieroglyphs = "";

            for (int i = 0; i < length; i++)
            {
                char currentCharacter = strings[0][i];

                if (strings.All(s => s.Contains(currentCharacter)) && !commonHieroglyphs.ToString().Contains(currentCharacter))
                {
                    commonHieroglyphs += currentCharacter;
                }
            }

            return commonHieroglyphs;
        }
    }


}

