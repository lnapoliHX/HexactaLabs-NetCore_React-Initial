

rundotnet() {
    dotnet restore &
    dotnet build &
    dotnet run --project ./Stock.Api/Stock.Api.csproj;
}

rundotnet
read -p "Pressione una tecla ..."