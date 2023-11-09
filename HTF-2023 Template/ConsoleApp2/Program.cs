using Common;
using System.Text.Json;

ChallengeA2 challengeA2 = new ChallengeA2();

//var response = await challengeA2.StartChallengeA2();
var navigation = await challengeA2.GetNavigation();
var response = await challengeA2.NavigateVines(navigation.AmountOfVines, navigation.Start, navigation.Directions);
Console.WriteLine(response.StatusCode);