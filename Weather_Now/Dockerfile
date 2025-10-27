# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY Weather_Now/*.csproj ./Weather_Now/
RUN dotnet restore Weather_Now/Weather_Now.csproj

# Copy everything else and build the release
COPY . .
RUN dotnet publish Weather_Now/Weather_Now.csproj -c Release -o out

# Use the official .NET runtime image for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Weather_Now.dll"]
