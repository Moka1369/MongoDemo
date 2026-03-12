FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["MongoDemo.Api/MongoDemo.Api.csproj", "MongoDemo.Api/"]
COPY ["MongoDemo.Application/MongoDemo.Application.csproj", "MongoDemo.Application/"]
COPY ["MongoDemo.Domain/MongoDemo.Domain.csproj", "MongoDemo.Domain/"]
COPY ["MongoDemo.Infrastrukture/MongoDemo.Infrastrukture.csproj", "MongoDemo.Infrastrukture/"]

RUN dotnet restore "MongoDemo.Api/MongoDemo.Api.csproj"

COPY . .

WORKDIR "/src/MongoDemo.Api"

RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

#dein Projektname:"MongoDemo.Api.dll"
ENTRYPOINT ["dotnet", "MongoDemo.Api.dll"]