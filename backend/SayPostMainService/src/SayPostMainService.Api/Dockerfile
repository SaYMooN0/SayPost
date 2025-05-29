FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["SayPostMainService/src/SayPostMainService.Api/SayPostMainService.Api.csproj", "SayPostMainService/src/SayPostMainService.Api/"]
RUN dotnet restore "SayPostMainService/src/SayPostMainService.Api/SayPostMainService.Api.csproj"

COPY . .
WORKDIR "/src/SayPostMainService/src/SayPostMainService.Api"
RUN dotnet build "SayPostMainService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SayPostMainService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SayPostMainService.Api.dll"]
