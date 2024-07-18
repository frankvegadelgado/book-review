FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app
COPY . .

ENTRYPOINT ["dotnet", "BookReview.Web.Host.dll"]
