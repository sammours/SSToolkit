To upload Nuget package:

1. Change to the folder containing the .nupkg file. (bin/debug)

2. Run the following command, specifying your package name (unique package ID) and replacing the key value with your API key:

```
dotnet nuget push <package-name>.1.0.0.nupkg --api-key <api-key> --source https://api.nuget.org/v3/index.json
```

3. dotnet displays the results of the publishing process

see https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli
