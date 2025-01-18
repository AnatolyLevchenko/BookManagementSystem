FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false

COPY ["BookManagementSystem.Client/BookManagementSystem.Client.csproj", "BookManagementSystem.Client/"]
COPY ["BookManagement.Shared/BookManagement.Shared.csproj", "BookManagement.Shared/"]
RUN dotnet restore "BookManagementSystem.Client/BookManagementSystem.Client.csproj"

COPY . .
WORKDIR "/src/BookManagementSystem.Client"

RUN dotnet build "BookManagementSystem.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookManagementSystem.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

RUN apk add jq
COPY ["BookManagementSystem.Client/script.sh", "/docker-entrypoint.d/35-script.sh"]
RUN chmod +x /docker-entrypoint.d/35-script.sh

COPY --from=publish /app/publish/wwwroot . 

EXPOSE 80
CMD /docker-entrypoint.d/35-script.sh && nginx -g "daemon off;"
