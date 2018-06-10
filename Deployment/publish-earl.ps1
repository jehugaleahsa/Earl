&dotnet pack "..\Earl\Earl.csproj" --configuration Release --output $PWD

.\NuGet.exe push Earl.*.nupkg -Source https://www.nuget.org/api/v2/package

Remove-Item Earl.*.nupkg