

rundotnet() {
    dotnet restore &
    dotnet build &
    dotnet run --project Stock.Api/Stock.Api.csproj;
}

rundotnet