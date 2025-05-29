FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["SayPostNotificationService/src/SayPostNotificationService.Api/SayPostNotificationService.Api.csproj", "SayPostNotificationService/src/SayPostNotificationService.Api/"]
RUN dotnet restore "SayPostNotificationService/src/SayPostNotificationService.Api/SayPostNotificationService.Api.csproj"

COPY . .
WORKDIR "/src/SayPostNotificationService/src/SayPostNotificationService.Api"
RUN dotnet build "SayPostNotificationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SayPostNotificationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SayPostNotificationService.Api.dll"]
