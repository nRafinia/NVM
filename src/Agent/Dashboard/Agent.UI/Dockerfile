FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Agent/Dashboard/Agent.UI/Agent.UI.csproj", "Agent/Dashboard/Agent.UI/"]
RUN dotnet restore "Agent/Dashboard/Agent.UI/Agent.UI.csproj"
COPY . .
WORKDIR "/src/Agent/Dashboard/Agent.UI"
RUN dotnet build "Agent.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Agent.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Agent.UI.dll"]
