using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring.Elo
{
    public class EloScoringStrategy : IScoringStrategy
    {
        /// <summary> 
        /// Also known as K in the ELO formula - the max change in rating from one game 
        /// </summary>
        private readonly Points _maxRatingChange;

        /// <summary>
        /// The difference in rating where one person is almost certain to win 
        /// </summary>
        private readonly Points _maximumSkillGap;
        
        /// <summary>
        /// The number of points a new player will start with when they join the league
        /// </summary>
        private readonly Points _newPlayerStartingRating;

        /// <summary>
        /// Creates a new scoring strategy based on the Elo methodology used in chess rankings
        /// See: https://en.wikipedia.org/wiki/Elo_rating_system
        /// This strategy has been adapted to work for game with more than 2 players by resolving 
        /// all pairwise games and dividing the proposed rating change by the number of players in the game.
        /// See Approach 3 in this article: http://www.gautamnarula.com/rating/
        /// </summary>
        /// <param name="maxRatingChange">The total change in points that can happen in a single game</param>
        /// <param name="maxSkillGap">The number of points difference between players before A should always beat B</param>
        /// <param name="startingRating">The number of points a new player will start with</param>
        public EloScoringStrategy(Points maxRatingChange, Points maxSkillGap, Points startingRating)
        {
            _maxRatingChange = maxRatingChange;
            _maximumSkillGap = maxSkillGap;
            _newPlayerStartingRating = startingRating;
        }

        /// <summary>
        /// Does nothing in this scoring strategy
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        /// Sets player's <see cref="Position"/>s based on who has the most points
        /// </summary>
        public void SetPositions(IList<PlayerScore> leaderBoard)
        {
            leaderBoard = leaderBoard.OrderByDescending(p => p.Points).ToList();

            var j = 0;

            for (var i = 0; i < leaderBoard.Count; i++)
            {
                var position = (i > 0 && leaderBoard[i].Points == leaderBoard[i - 1].Points)
                    ? new Position(j)
                    : new Position(++j);

                leaderBoard[i].Position = position;
            }
        }

        /// <summary>
        /// Updates the provided scoreboard with the results of a <see cref="Game"/>
        /// A player will gain up to <see cref="_maxRatingChange"/> based on their probablity
        /// of winning against each other player.
        /// </summary>
        public IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game)
        {
            var results = game.GetResults();
            var previousScores = new Dictionary<string, Points>();

            foreach (var playerName in results.Select(x => x.Name))
            {
                var player = scoreboard.SingleOrDefault(p => p.Name == playerName);

                if (player == null)
                {
                    player = new PlayerScore(playerName);
                    scoreboard.Add(player);
                    player.Points = _newPlayerStartingRating;
                    player.Position = new Position(0);
                }

                previousScores.Add(playerName, player.Points);
            }

            foreach (var playerAName in results.Select(x => x.Name))
            {
                foreach (var playerBName in results.Select(x => x.Name))
                {
                    if (playerAName == playerBName) continue;

                    var playerAResult = results.Single(x => x.Name == playerAName);
                    var playerBResult = results.Single(x => x.Name == playerBName);

                    var playerA = scoreboard.Single(p => p.Name == playerAName);

                    var chanceOfPlayerAWinning = ChanceOfWinning(previousScores[playerAName], previousScores[playerBName]);

                    // If the players have drawn then don't update their scores
                    if (PlayersDraw(playerAResult, playerBResult))
                    {
                        continue;
                    }

                    var didPlayerAWin = PlayerAWon(playerAResult, playerBResult);
                    var adjustedRatingChange = RatingChange(chanceOfPlayerAWinning, didPlayerAWin, results.Count);
                    var integerRatingChange = Math.Round(adjustedRatingChange, MidpointRounding.AwayFromZero);

                    playerA.AddPoints(new Points(integerRatingChange));
                }
            }

            return scoreboard;
        }

        /// <summary>
        /// Did player A and player B draw in this game
        /// </summary>
        private static bool PlayersDraw(PlayerScore playerAResult, PlayerScore playerBResult)
        {
            if (playerAResult.Points == new Points(0) &&
                playerAResult.Position != new Position(0))
            {
                return playerAResult.Position == playerBResult.Position;
            }

            return playerAResult.Points == playerBResult.Points;
        }

        /// <summary>
        /// Did player A beat player B in this game
        /// </summary>
        private static bool PlayerAWon(PlayerScore playerAResult, PlayerScore playerBResult)
        {
            if (playerAResult.Points == new Points(0) &&
                playerAResult.Position != new Position(0))
            {
                return playerAResult.Position > playerBResult.Position;
            }

            return playerAResult.Points > playerBResult.Points;

        }

        /// <summary>
        /// Get the number of points to add or remove from a player
        /// </summary>
        private double RatingChange(double expectedToWin, bool actuallyWon, int totalPlayers)
        {
            var w = (actuallyWon) ? 1 : 0;
            return _maxRatingChange * new Points(w - expectedToWin) / (totalPlayers - 1);
        }

        /// <summary>
        /// the chance of a player with rating <param name="ratingA"/> beating a player with rating <param name="ratingB"/>
        /// </summary>
        /// <remarks>
        /// See https://www.wolframalpha.com/input/?i=plot+1%2F%281+%2B+Pow%2810%2C+%28y+-+x%29%2F400%29%29%3B
        /// for a graph of this function.
        /// </remarks>
        private double ChanceOfWinning(Points ratingA, Points ratingB)
        {
            return 1 /(1 + Points.Pow(10.0, ((ratingB - ratingA) / _maximumSkillGap)));
        }
    }
}