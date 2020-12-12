FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.12 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.12 AS build
WORKDIR /app
COPY ./src/API.Base.Api/API.Base.Api.csproj ./src/API.Base.Api/
COPY ./src/API.Base.Core/API.Base.Core.csproj ./src/API.Base.Core/
COPY ./src/API.Base.Data/API.Base.Data.csproj ./src/API.Base.Data/
COPY ./src/API.Base.External/API.Base.External.csproj ./src/API.Base.External/
COPY ./src/API.Base.Realtime/API.Base.Realtime.csproj ./src/API.Base.Realtime/
COPY ./src/API.Base.Service/API.Base.Service.csproj ./src/API.Base.Service/
RUN dotnet restore src/API.Base.Api/API.Base.Api.csproj
COPY . .
RUN dotnet build src/API.Base.Api/API.Base.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish src/API.Base.Api/API.Base.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.Base.Api.dll"]