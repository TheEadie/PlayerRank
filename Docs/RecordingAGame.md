## Recording a Game

Warning: Mixing results recorded by `Position` and `Points` is not advised in a single game

### By Position

To record a game based on positions:

```
var game = new Game();
game.AddResult("Foo", new Position(1));
game.AddResult("Bar", new Position(2));
```

or make use of helper properties

```
var game = new Game();
game.AddResult("Foo", Position.First);
game.AddResult("Bar", Position.Second);
```

### By Points/Score

To record a game based on points:
```
var game = new Game();
game.AddResult("Foo", new Points(10));
game.AddResult("Bar", new Points(0));
```
### Record the game in a league

To add the game to a league:

```
var league = new League();
league.RecordGame(game);
```
