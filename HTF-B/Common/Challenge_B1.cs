using System;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    public class Challenge_B1
    {
        private HackTheFutureClient client;

        public Challenge_B1(HackTheFutureClient client)
        {
            this.client = client;
        }

        public async Task StartChallenge()
        {
            await client.Login("Team A", "SZRz9Zqpiv");
            await client.GetAsync("/api/path/b/easy/start");
        }

        public async Task TestSample()
        {
            await client.Login("Team A", "SZRz9Zqpiv");
            var response = await client.GetAsync("/api/path/b/easy/puzzle");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var calendar = JsonConvert.DeserializeObject<MayanCalendarChallengeDto>(content);

                var count = GetNumberOfTimesDayAppears(calendar.StartDate, calendar.EndDate, calendar.Day);

                var postResponse = await client.PostAsJsonAsync($"/api/path/b/easy/puzzle", count);

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

        public int GetNumberOfTimesDayAppears(DateOnly startDate, DateOnly endDate, string day)
        {
            var count = 0;
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek.ToString() == day)
                {
                    count++;
                }
            }
            return count;
        }
    }
}

