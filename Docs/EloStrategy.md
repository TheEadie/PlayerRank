## Elo Scoring Strategy

The Elo scoring strategy is a slightly adapted version of the rating system used to rank chess players.
The startegy provided here has been adapted to work for games where more than 2 players take part. 

### Overview

Players are assigned a rating based on their relative performance within the league. After each game a player may gain or lose rating based on how likely it was that they would finish in the position they did. 
See https://en.wikipedia.org/wiki/Elo_rating_system for details.

### Paramaters

`maxRatingChange` The largest total number of points a player can gain in a single game.
`maxSkillGap` The total number points diffrence between two players until one of them is 100% likely to win.
`startingRating` The number of points that a new player will start with.

### Example

```
var game = new Game();
game.AddResult("Foo", Position.First);
game.AddResult("Bar", Position.Second);

var league = new League();
league.RecordGame(game);

var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));

foreach (var position in league.GetLeaderBoard(eloScoringStrategy))
{
    Console.WriteLine("Position: {0}, Name: {1}, Score: {2}",
        position.Position, position.Name, position.Points);
}
```