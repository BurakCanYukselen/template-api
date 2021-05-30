FROM mcr.microsoft.com/dotnet/sdk:5.0.300-alpine3.13 AS build_env
FROM mcr.microsoft.com/dotnet/aspnet:5.0.6-alpine3.13 AS run_env
EXPOSE 80

FROM build_env AS build
WORKDIR /app
COPY . ./
ENV PROJECT_NAME="API.Base.Api"
RUN dotnet restore src/${PROJECT_NAME}/${PROJECT_NAME}.csproj
RUN dotnet publish src/${PROJECT_NAME}/${PROJECT_NAME}.csproj -c Release -o /app/publish

FROM run_env AS run
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.Base.Api.dll"]