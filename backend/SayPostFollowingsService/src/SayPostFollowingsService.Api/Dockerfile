FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["SayPostFollowingsService/src/SayPostFollowingsService.Api/SayPostFollowingsService.Api.csproj", "SayPostFollowingsService/src/SayPostFollowingsService.Api/"]
RUN dotnet restore "SayPostFollowingsService/src/SayPostFollowingsService.Api/SayPostFollowingsService.Api.csproj"

COPY . .
WORKDIR "/src/SayPostFollowingsService/src/SayPostFollowingsService.Api"
RUN dotnet build "SayPostFollowingsService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SayPostFollowingsService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SayPostFollowingsService.Api.dll"]
