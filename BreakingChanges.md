### Breaking Changes v2.x.x to v3.0.0

#### Upgrading from v2.0.0 +

- PlayerRank now targets .Net Standard 1.0. This should require no changes to your code but may bring additional dependencies when you upgrade.

### Breaking Changes v1.x.x to v2.0.0

#### Upgrading from v1.1.0 +

- `LowestPointsScoringStrategy` has been removed due to a number of bugs. This will return in a later version.

#### Upgrading from v1.0.0 +

- Scoring strategies now have their own namespaces
 - `SimpleScoringStrategy` is now located in `PlayerRank.Scoring.Simple`
 - `EloScoringStrategy` is now located in `PlayerRank.Scoring.Elo`
- `Game.AddResult(string, double)` has been removed, please use `Game.AddResult(string, Points)` instead
- `PlayerScore.Score` has been removed, please use `PlayerScore.Points` instead
- `EloScoringStrategy(double, double, double)` has been removed, please use `EloScoringStrategy(Points, Points, Points)` instead