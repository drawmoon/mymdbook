FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
COPY WebApplication.sln .
COPY WebApplication WebApplication
RUN dotnet restore \
  && dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS prod
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "WebApplication.dll" ]