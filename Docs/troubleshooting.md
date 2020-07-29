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

- Si tienen algún tipo de error de dependencias:
Click derecho en la Solución -> Restore NuGet Packages