## Simple Scoring Strategy 

### Overview

The simple scoring strategy awards points based on the position each player finishes, or totals the points recorded in each game if this is provided instead.

### Paramaters

`pointsMap` A mapping of `Position`s to the number of `Points` awarded. This defaults to providing 10 points for 1st down to 1 point for 10th.

### Example

```
var game = new Game();
game.AddResult("Foo", Position.First);
game.AddResult("Bar", Position.Second);

var league = new League();
league.RecordGame(game);

var scoringStrategy = new SimpleScoringStrategy();

foreach (var position in league.GetLeaderBoard(scoringStrategy))
{
    Console.WriteLine("Position: {0}, Name: {1}, Score: {2}",
        position.Position, position.Name, position.Points);
}
```