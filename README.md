![Build](https://github.com/TheEadie/PlayerRank/workflows/Build/badge.svg)

# PlayerRank
Calculates rankings for multiplayer games

### How to install

`dotnet add package PlayerRank`

### Basic usage

```
var game = new Game();
game.AddResult("Foo", new Points(10));
game.AddResult("Bar", new Points(0));

var league = new League();
league.RecordGame(game);

var scoringStrategy = new SimpleScoringStrategy();

foreach (var position in league.GetLeaderBoard(scoringStrategy))
{
    Console.WriteLine("Position: {0}, Name: {1}, Score: {2}",
        position.Position, position.Name, position.Points);
}
```

### Documentation

- [Recording Games](docs/RecordingAGame.md)
- [Simple Scoring Strategy](docs/SimpleStrategy.md)
- [Elo Scoring Strategy](docs/EloStrategy.md)

### Report an issue or suggest an improvement

Please add to the issue tracker here on GitHub or send me a pull request with your improvements.
