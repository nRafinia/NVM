FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Agent/Console/CMD_Test/CMD_Test.csproj", "Agent/Console/CMD_Test/"]
RUN dotnet restore "Agent/Console/CMD_Test/CMD_Test.csproj"
COPY . .
WORKDIR "/src/Agent/Console/CMD_Test"
RUN dotnet build "CMD_Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CMD_Test.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMD_Test.dll"]
