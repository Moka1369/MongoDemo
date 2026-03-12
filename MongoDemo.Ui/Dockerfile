FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["MongoDemo.Ui/MongoDemo.Ui.csproj", "MongoDemo.Ui/"]
RUN dotnet restore "MongoDemo.Ui/MongoDemo.Ui.csproj"

COPY . .
WORKDIR "/src/MongoDemo.Ui"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MongoDemo.Ui.dll"]