
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GameChallenge.Web/GameChallenge.Web.csproj", "GameChallenge.Web/"]
COPY ["GameChallenge.Infrastructure/GameChallenge.Infrastructure.csproj", "GameChallenge.Infrastructure/"]
COPY ["GameChallenge.Core/GameChallenge.Core.csproj", "GameChallenge.Core/"]
RUN dotnet restore "GameChallenge.Web/GameChallenge.Web.csproj"
COPY . .
WORKDIR "/src/GameChallenge.Web"
RUN dotnet build "GameChallenge.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameChallenge.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameChallenge.Web.dll"]
