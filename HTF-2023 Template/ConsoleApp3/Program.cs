using Common;
using System.Text.Json;

ChallengeA3 challengeA3 = new ChallengeA3();

//var response = await challengeA3.StartChallengeA3();

//Console.WriteLine(response.StatusCode);

var animals = await challengeA3.GetAnimals();

var response = await challengeA3.BuildChain();
Console.WriteLine(response.StatusCode);
