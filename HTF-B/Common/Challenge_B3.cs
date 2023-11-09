using System;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Common
{
	public class Challenge_B3
	{
        private HackTheFutureClient client;

        public Challenge_B3(HackTheFutureClient client)
        {
            this.client = client;
        }

        public async Task StartChallenge()
        {
            await client.Login("Team A", "SZRz9Zqpiv");
            await client.GetAsync("/api/path/b/hard/start");
        }

        public async Task TestSample()
        {
            await client.Login("Team A", "SZRz9Zqpiv");
            var response = await client.GetAsync("/api/path/b/hard/sample");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var strings = JsonConvert.DeserializeObject<string[]>(content);

                var commonHieroglyphs = GetSumOfNumber(strings);

                var postResponse = await client.PostAsJsonAsync($"/api/path/b/hard/sample", commonHieroglyphs);

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


        public string GetSumOfNumber(string[] numbers)
        {
            int sum = 0;
            string collection = "";

            foreach (var number in numbers)
            {
                collection += number;
            }

            if (collection.Length == 0)
            {
                Console.WriteLine("empty collection");
            }
            else
            {
                for (int i = 0; i < collection.Length; i++)
                {
                    if (collection[i] == '.')
                    {
                        sum += (int)Math.Pow(1, i);
                    }
                    if (collection[i] == '|')
                    {
                        sum += (int)Math.Pow(5, i);
                    }
                    if (collection[i] == ' ')
                    {
                        var value = collection[i - 1] + collection[i + 1];
                        sum += value;
                    }
                    if (collection[i] == 'Ⱄ')
                    {
                        sum += (int)Math.Pow(0, i);
                    }
                }
            }



            return sum.ToString();   
        }

    }
}

