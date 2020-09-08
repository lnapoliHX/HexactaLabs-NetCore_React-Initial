# Troubleshooting

[Volver](./index.md)

## Posibles problemas: 
- Para crear los assets para buildear: 
ctrl+alt+p => Net generate assets

- Si vscode no les carga c# y les muestra un error de versión 

Se resuelve con éste issue: 
https://stackoverflow.com/questions/55535177/omnisharp-msbuild-projectmanager-attempted-to-update-project-that-is-not-loaded

The SDK 'Microsoft.NET.Sdk.Web' specified could not be found.
https://github.com/OmniSharp/omnisharp-roslyn/issues/1313#issuecomment-429039879

En Entornos linux:  Opción 2) agregar MsBuild 

en .bashrc o según corresponda: 

```
export MSBuildSDKsPath="/usr/share/dotnet/sdk/3.1.401/Sdks/"
export PATH=$MSBuildSDKsPath:$PATH
```

Dependiendo la versión la podríamos agregar así: 
```
export MSBuildSDKsPath="/usr/share/dotnet/sdk/$(dotnet --version)/Sdks/"
export PATH=$MSBuildSDKsPath:$PATH
```

https://github.com/OmniSharp/omnisharp-vscode/issues/2604#issuecomment-429437465

- Si tienen algún tipo de error de dependencias:
Click derecho en la Solución -> Restore NuGet Packages