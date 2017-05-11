&dotnet pack "..\src\Earl\project.json" --configuration Release --output .

Remove-Item Earl.*.symbols.nupkg

.\NuGet.exe push Earl.*.nupkg -Source https://www.nuget.org/api/v2/package

Remove-Item Earl.*.nupkg