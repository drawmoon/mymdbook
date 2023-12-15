FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY BlazorApp1.sln .
COPY BlazorApp1 BlazorApp1
RUN dotnet restore \
  && dotnet publish -c Release -o /app --no-restore

FROM nginx:stable AS prod
COPY --from=build /app/wwwroot /usr/share/nginx/html