using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ChallengeA2
    {
        HackTheFutureClient _client;

        public ChallengeA2()
        {
            _client = new HackTheFutureClient();
        }
        public async Task<HttpResponseMessage> StartChallengeA2()
        {
            await _client.Login("Team A", "SZRz9Zqpiv");
            var response = await _client.GetAsync("/api/path/a/medium/start");

            return response;
        }

        public async Task<VineNavigationChallengeDto> GetNavigation()
        {
            await _client.Login("Team A", "SZRz9Zqpiv");
            var response = await _client.GetFromJsonAsync<VineNavigationChallengeDto>("/api/path/a/medium/puzzle");
            return response;
        }

        public async Task<HttpResponseMessage> NavigateVines(int amountOfVines, string start, string[] directions)
        {
            await _client.Login("Team A", "SZRz9Zqpiv");

            var maxBoundary = Math.Sqrt(amountOfVines) - 1;
            string[] startArray = start.Split(',');
            int x = int.Parse(startArray[0]);
            int y = int.Parse(startArray[1]);
            foreach (var direction in directions)
            {
                switch (direction)
                {
                    case "U":
                        if (y < maxBoundary) y += 1;
                        break;
                    case "D":
                        if (y > 0) y -= 1;
                        break;
                    case "L":
                        if (x > 0) x -= 1;
                        break;
                    case "R":
                        if (x < maxBoundary) x += 1;
                        break;
                    case "UL":
                        if (x > 0 && y < maxBoundary)
                        {
                            x -= 1;
                            y += 1;
                        }
                        break;
                    case "UR":
                        if (x < maxBoundary && y < maxBoundary)
                        {
                            x += 1;
                            y += 1;
                        }
                        break;
                    case "DL":
                        if (x > 0 && y > 0)
                        {
                            x -= 1;
                            y -= 1;
                        }
                        break;
                    case "DR":
                        if (x < maxBoundary && y > 0)
                        {
                            x += 1;
                            y -= 1;
                        }
                        break;
                    default:
                        // Handle invalid input
                        break;
                }
            }

            string finalCoordinates = $"{x},{y}";

            var response = await _client.PostAsJsonAsync("/api/path/a/medium/puzzle", finalCoordinates);

            return response;
        }
    }
}
