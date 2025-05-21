# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY MelikeCoffee/*.csproj ./MelikeCoffee/
RUN dotnet restore

COPY . .
WORKDIR /app/MelikeCoffee
RUN dotnet publish -c Release -o out

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/MelikeCoffee/out ./

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "MelikeCoffee.dll"]
