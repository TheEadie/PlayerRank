GitVersion /output buildserver

$VersionJson = GitVersion | out-string
$Versions = ConvertFrom-Json $VersionJson

$Version = $Versions.MajorMinorPatch
$NuGetVersion = $Versions.NuGetVersion

dotnet build -c Release /p:Version=$Version
dotnet test src/PlayerRank.Tests/PlayerRank.Tests.csproj --logger "trx;LogFileName=PlayerRank.Tests.trx"
dotnet pack src\PlayerRank\PlayerRank.csproj -c Release /p:Version=$Version /p:PackageVersion=$NuGetVersion