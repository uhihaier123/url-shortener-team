# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/UrlShortener.Api/UrlShortener.Api.csproj", "src/UrlShortener.Api/"]
RUN dotnet restore "src/UrlShortener.Api/UrlShortener.Api.csproj"
COPY . .
WORKDIR "/src/src/UrlShortener.Api"
RUN dotnet publish "UrlShortener.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "UrlShortener.Api.dll"]
