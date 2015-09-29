### Breaking Changes V1.x.x to V2.0.0

#### Upgrading from V1.0.0 +

- Scoring strategies now have their own namespaces
 - `SimpleScoringStrategy` is now located in `PlayerRank.Scoring.Simple`
 - `EloScoringStrategy` is now located in `PlayerRank.Scoring.Elo`
- `Game.AddResult(string, double)` has been removed, please use `Game.AddResult(string, Points)` instead
- `PlayerScore.Score` has been removed, please use `PlayerScore.Points` instead
- `EloScoringStrategy(double, double, double)` has been removed, please use `EloScoringStrategy(Points, Points, Points)` instead

#### Upgrading from V1.1.0 +

- `LowestPointsScoringStrategy` has been removed due to a number of bugs. This will return in a later version.