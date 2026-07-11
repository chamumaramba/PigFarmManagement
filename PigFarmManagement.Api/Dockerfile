FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5017

ENV ASPNETCORE_URLS=http://+:5017

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["PigFarmManagement.Api/PigFarmManagement.Api.csproj", "PigFarmManagement.Api/"]
RUN dotnet restore "PigFarmManagement.Api/PigFarmManagement.Api.csproj"
COPY . .
WORKDIR "/src/PigFarmManagement.Api"
RUN dotnet build "PigFarmManagement.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "PigFarmManagement.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PigFarmManagement.Api.dll"]
