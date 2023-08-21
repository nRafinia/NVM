FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Agent/AgentService/AgentService.API/AgentService.API.csproj", "Agent/AgentService/AgentService.API/"]
RUN dotnet restore "Agent/AgentService/AgentService.API/AgentService.API.csproj"
COPY . .
WORKDIR "/src/Agent/AgentService/AgentService.API"
RUN dotnet build "AgentService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgentService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgentService.API.dll"]
