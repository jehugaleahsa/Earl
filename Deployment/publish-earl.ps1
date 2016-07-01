&dotnet pack "..\src\Earl\project.json" --configuration Release --output .

Remove-Item Earl.*.symbols.nupkg

.\NuGet.exe push Earl.*.nupkg -Source nuget.org

Remove-Item Earl.*.nupkg