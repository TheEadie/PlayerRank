[![Build Status](https://eadie.visualstudio.com/PlayerRank/_apis/build/status/PlayerRank%20-%20Build?branchName=master)](https://eadie.visualstudio.com/PlayerRank/_build/latest?definitionId=4&branchName=master)

# PlayerRank
Calculates rankings for multiplayer games

### How to install

Run the following command in Nuget Package Manager Console: `Install-Package PlayerRank`

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

- [Recording Games](Docs/RecordingAGame.md)
- [Simple Scoring Strategy](Docs/SimpleStrategy.md)
- [Elo Scoring Strategy](Docs/EloStrategy.md)

### Report an issue or suggest an improvement

Please add to the issue tracker here on GitHub or send me a Pull Request with your improvements.
