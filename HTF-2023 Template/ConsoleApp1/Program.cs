using Common;

ChallengeA1 challengeA1 = new ChallengeA1();

//var response = await challengeA1.StartChallengeA1();

//Console.WriteLine(response.StatusCode);
var hieroglyphString = await challengeA1.GetHieroglyph();



var response = await challengeA1.PostHieroglyph(challengeA1.Decipher(hieroglyphString));
Console.WriteLine(response.StatusCode);