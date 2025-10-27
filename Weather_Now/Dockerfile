# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY Weather_Now/*.csproj ./Weather_Now/
RUN dotnet restore Weather_Now/Weather_Now.csproj
COPY . .
RUN dotnet publish Weather_Now/Weather_Now.csproj -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Weather_Now.dll"]
