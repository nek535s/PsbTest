using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace PsbTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchResultsController : ControllerBase
    {
        [HttpGet]
        public IActionResult CalculateTeamScores()
        {
            string[] matchesT = { "3:1", "2:2", "0:1", "4:2", "3:a", "3- 12" };

            List<string> validMatches = new List<string>();

            List<Dictionary<string, object>> matchResults = new List<Dictionary<string, object>>();

            foreach (string match in matchesT)
            {
                string[] scores = match.Split(':');
                if (scores.Length == 2)
                {
                    if (int.TryParse(scores[0], out int goalsTeam1) && int.TryParse(scores[1], out int goalsTeam2))
                    {
                        validMatches.Add(match);
                    }
                }
            }

            foreach (string matchesFinal in validMatches)
            {
                string[] matchResult = matchesFinal.Split(':');

                Dictionary<string, object> matchData = new Dictionary<string, object>();

                matchData.Add("Match", matchesFinal);

                try
                {
                    int goalsTeam1 = Int32.Parse(matchResult[0]);
                    int goalsTeam2 = Int32.Parse(matchResult[1]);

                    Dictionary<string, int> teamScores = new Dictionary<string, int>();

                    if (goalsTeam1 > goalsTeam2)
                    {
                        teamScores["Team 1"] = 3;
                        teamScores["Team 2"] = 0;
                    }
                    else if (goalsTeam1 < goalsTeam2)
                    {
                        teamScores["Team 1"] = 0;
                        teamScores["Team 2"] = 3;
                    }
                    else
                    {
                        teamScores["Team 1"] = 1;
                        teamScores["Team 2"] = 1;
                    }

                    matchData.Add("TeamScores", teamScores);
                    matchResults.Add(matchData);
                }
                catch (FormatException)
                {
                    continue;
                }
            }
            return Ok(matchResults);
        }
    }
}