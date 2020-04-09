# Install pre-reqs
dotnet tool install -g GitVersion.Tool

# Calculate the version
GitVersionOutput=dotnet-gitversion
Version=$($GitVersionOutput | jq -r '.MajorMinorPatch')
NuGetVersion=$($GitVersionOutput | jq -r '.NuGetVersion')

# Build
echo "Building version:" $VERSION
dotnet build -c Release /p:Version=$Version

# Test
echo "Running tests"
dotnet test --logger "trx;LogFileName=PlayerRank.Tests.trx"

# Create NuGet Package
echo "Creating NuGet Package"
rm .artifacts/ -rf
dotnet pack -c Release -o ".artifacts/" /p:Version=$Version /p:PackageVersion=$NuGetVersion