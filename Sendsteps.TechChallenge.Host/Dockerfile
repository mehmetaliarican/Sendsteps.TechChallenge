#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["Sendsteps.TechChallenge.Host/Sendsteps.TechChallenge.Host.csproj", "Sendsteps.TechChallenge.Host/"]
RUN dotnet restore "Sendsteps.TechChallenge.Host/Sendsteps.TechChallenge.Host.csproj"
COPY . .
WORKDIR "/src/Sendsteps.TechChallenge.Host"
RUN dotnet build "Sendsteps.TechChallenge.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sendsteps.TechChallenge.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Sendsteps.TechChallenge.Host.dll"]