### Breaking Changes V3

- PlayerRank now targets .Net Standard 1.0. This should require no changes to your code but may bring additional dependencies when you upgrade.

### Breaking Changes V2

- `LowestPointsScoringStrategy` has been removed due to a number of bugs. This will return in a later version.
- Scoring strategies now have their own namespaces
 - `SimpleScoringStrategy` is now located in `PlayerRank.Scoring.Simple`
 - `EloScoringStrategy` is now located in `PlayerRank.Scoring.Elo`
- `Game.AddResult(string, double)` has been removed, please use `Game.AddResult(string, Points)` instead
- `PlayerScore.Score` has been removed, please use `PlayerScore.Points` instead
- `EloScoringStrategy(double, double, double)` has been removed, please use `EloScoringStrategy(Points, Points, Points)` instead