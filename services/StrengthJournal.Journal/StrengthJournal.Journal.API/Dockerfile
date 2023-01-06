#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StrengthJournal.Journal.API/StrengthJournal.Journal.API.csproj", "StrengthJournal.Journal.API/"]
RUN dotnet restore "StrengthJournal.Journal.API/StrengthJournal.Journal.API.csproj"
COPY . .
WORKDIR "/src/StrengthJournal.Journal.API"
RUN dotnet build "StrengthJournal.Journal.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StrengthJournal.Journal.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StrengthJournal.Journal.API.dll"]