FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY WebApplication1.sln .
COPY WebApplication1 WebApplication1
RUN dotnet restore \
  && dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS prod
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "WebApplication1.dll" ]