FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Agent/Console/Hardware_Info/Hardware_Info.csproj", "Agent/Console/Hardware_Info/"]
RUN dotnet restore "Agent/Console/Hardware_Info/Hardware_Info.csproj"
COPY . .
WORKDIR "/src/Agent/Console/Hardware_Info"
RUN dotnet build "Hardware_Info.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hardware_Info.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hardware_Info.dll"]
