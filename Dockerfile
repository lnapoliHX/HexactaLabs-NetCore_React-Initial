
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["Stock.Api/Stock.Api.csproj", "Stock.Api/"]
RUN dotnet restore "Stock.Api/Stock.Api.csproj"
COPY . ./
WORKDIR "/src/Stock.Api"
RUN dotnet build "Stock.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stock.Api.csproj" -c Release -o /app/publish

FROM base as final 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stock.Api.dll"]